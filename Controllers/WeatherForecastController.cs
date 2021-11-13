using System;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
