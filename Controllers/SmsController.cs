using System;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnetDockerHerokuTwilio.Controllers
{       


    [ApiController]
    [Route("[controller]")]
    public class SmsController : ControllerBase
    {
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            Dictionary<string, List<string>> roomCodes = new Dictionary<string, List<string>>();
            Dictionary<string, string> hosts = new Dictionary<string, string>();

            public int getRandomIndex(int length)
            {
                Random integer = new Random();
                return integer.Next(length);
            }
            public List<string> swap (List<string> phoneNumbers , int a, int b)
            {
                string temp = phoneNumbers[a];
                phoneNumbers[a] = phoneNumbers[b];
                phoneNumbers[b] = temp;
                return phoneNumbers;
            }
            public List<string> shuffle (List<string> phoneNumbers)
            {
                for(int i = 0; i < phoneNumbers.Count; i++)
                {
                    swap(phoneNumbers, i, getRandomIndex(phoneNumbers.Count));
                    swap(phoneNumbers, i, getRandomIndex(phoneNumbers.Count));
                }
                return phoneNumbers;
            }

        [HttpPost]
        public MessageResource Index([FromForm] SmsRequest incomingMessage)
        {
            Console.WriteLine("this is the incoming: " + incomingMessage.Body);

            TwilioClient.Init(accountSid, authToken);

            string unverifiedShape = incomingMessage.Body.Trim().ToLower();
            string notHostReg = @"^[a-zA-Z0-9]+$";
            string hostReg = "^[a-z1-9A-Z]+:(open|close)$";
            Regex reggie = new Regex(notHostReg);
            Regex hostReggie = new Regex(hostReg);

            if(!hostReggie.IsMatch(unverifiedShape))
            {// not a valid host. is it a valid participant?
                if(!reggie.IsMatch(unverifiedShape))
                {   
                    // body of message is not acceptable
                    var badRequestMessage = MessageResource.Create(
                        body:"OOPS! You've made a bad request, and this is unnacceptable. Your text must include only contain letters a-z, A-Z, numbers 1-9. Exmaple1 \"roomcode\" . or if your a host you may include a single colon. Example2 \"roomcode:open\" .",
                        from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                        to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                    );
                    return badRequestMessage;
                } 
                else
                {
                    // is not a host but a valid request to be a participant(possibly does not belong to a room)
                    if(roomCodes.TryGetValue(incomingMessage.Body, out List<string> validRoom))
                    {
                        validRoom.Add(incomingMessage.From);

                        var goodRequestMessage = MessageResource.Create(
                            body:"You  have been successfully added to: " + incomingMessage.Body + " Please wait for host to close room.",
                            from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                            to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                        );
                        return goodRequestMessage;
                    }
                    else
                    {
                        var badRequestMessage = MessageResource.Create(
                            body:"is not a host but a valid request to be a participant(possibly does not belong to a room)",
                            from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                            to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                        );
                        return badRequestMessage;
                    }

                }
            }
            else
            {
                // is valid host but command may be wrong for the situation of the room or roomcode might be wrong or lack permissions.
                string verifiedShape = unverifiedShape;
                string room = "";
                int i = 0;
                while(i < verifiedShape.Length && verifiedShape[i] != ':')
                {
                    room += verifiedShape[i];
                    i++;
                }
                if(i < verifiedShape.Length && verifiedShape[i]==':')
                {
                    if(verifiedShape[i+1] == 'o')
                    {
                        if(roomCodes.TryGetValue(room, out List<string> phoneNumbers))
                        {
                            // room is taken --> cannot open.
                            var roomTaken = MessageResource.Create(
                                body:"Sorry, this roomcode is already in use --> cannot open --> please open a room with a different code",
                                from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                                to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                            );  
                        }
                        else
                        {
                            // room is available --> initialize.
                            // roomCodes[room] = new List<string>();
                            if(roomCodes.TryAdd(room, new List<string>()))
                            {
                                var hasBeenOpened = MessageResource.Create(
                                    body:"Congratulations, anyone who texts: " + incomingMessage.Body + "to this service will be added to the room. To Close room and send everyone a random number just send a text to this service saying  " + incomingMessage.Body + ":close",
                                    from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                                    to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                                ); 
                                return hasBeenOpened;
                            } 
                            else
                            {
                                var hasNotBeenOpened = MessageResource.Create(
                                    body:"I'm sorry we've encountered an error opening this room. " + incomingMessage.Body + "Did not work. Try something else maybe?(SORRY!)",
                                    from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                                    to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                                ); 
                                return hasNotBeenOpened;
                            }


                        }

                    } 
                    else
                    {
                        // reads "close
                        List<string> shuffled = shuffle(roomCodes[room]);
                        for(int i = 0; i < shuffled.Count; i++)
                        {
                        var saysClose = MessageResource.Create(
                            body:$"{i}",
                            from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                            to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable(shuffled[i]))
                            );   
                        }
\                         var worked = MessageResource.Create(
                            body:$"{i}",
                            from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                            to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                            );  
                        return worked;
                    }

                }
                   
            }


         
            
            
            
            
            
   

            var message = MessageResource.Create(
                body: "this shit went all the way through space for you to say this: " + incomingMessage.Body + "??????",
                from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
            );
            return message;
        }

        [HttpGet]
        public string Get()
        {
        TwilioClient.Init(accountSid, authToken);
        var message = MessageResource.Create(
            body: "Join Earth's mightiest heroes. Like Kevin Bacon Advocado.",
            from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
            to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
        );
            return "yoyoyoyoyoyoyoooyoyyooyyoyoyyoyoy is this your body?? really???"+Environment.GetEnvironmentVariable("TESTING");
        }

    }
    //  public class SmsController : TwilioController
//     {
//         public TwiMLResult Index(SmsRequest incomingMessage)
//         {
//             Console.WriteLine("made it to 19" + incomingMessage);
//             var messagingResponse = new MessagingResponse();
//             messagingResponse.Message("The chopy cattch says: " +
//                                       incomingMessage.Body);

//             return TwiML(messagingResponse);
//         }
//     }

}
