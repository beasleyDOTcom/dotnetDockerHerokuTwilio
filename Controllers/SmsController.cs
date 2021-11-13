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
                } 
                else
                {
                    // is not a host but a valid request to be a participant(possibly does not belong to a room)
                    var badRequestMessage = MessageResource.Create(
                        body:"is not a host but a valid request to be a participant(possibly does not belong to a room)",
                        from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                        to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                    );
                }
            }
            else
            {
                // is valid host but command may be wrong for the situation of the room or roomcode might be wrong or lack permissions.
                var badRequestMessage = MessageResource.Create(
                    body:"is valid host but command may be wrong for the situation of the room or roomcode might be wrong or lack permissions.",
                    from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                    to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                );    
                   
            }

            string verifiedShape = unverifiedShape;
            string room = "";
            int i = 0;
            while(i < verifiedShape.Length && verifiedShape[i] != ':')
            {
                room += verifiedShape[i];
                i++;
            }
            if(verifiedShape[i]==':')
            {
                if(verifiedShape[i+1] == 'o')
                {
                var saysOpen = MessageResource.Create(
                    body:"says open",
                    from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                    to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                );  
                return saysOpen;
                } 
                else
                {
                    // reads "close
                var saysClose = MessageResource.Create(
                    body:"says  close",
                    from: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("SERVER_NUMBER")),
                    to: new Twilio.Types.PhoneNumber(Environment.GetEnvironmentVariable("PERSONAL_NUMBER"))
                );           
                return saysClose;           
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
