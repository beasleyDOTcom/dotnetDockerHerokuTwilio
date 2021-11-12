using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace dotnetDockerHerokuTwilio.Controllers
{
    [ApiController]
    [Route("[controller]")]
        public class SmsController : TwilioController
    {
        public TwiMLResult Index(SmsRequest incomingMessage)
        {
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("The chopy cattch says: " +
                                      incomingMessage.Body);

            return TwiML(messagingResponse);
        }
    }
    // public class SmsController : ControllerBase
    // {


    //     private static readonly string[] Summaries = new[]
    //     {
    //         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //     };

    //     private readonly ILogger<SmsController> _logger;

    //     public SmsController(ILogger<SmsController> logger)
    //     {
    //         _logger = logger;
    //     }

    //     [HttpGet]
    //     public IEnumerable<WeatherForecast> Get()
    //     {
    //         var rng = new Random();
    //         return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateTime.Now.AddDays(index),
    //             TemperatureC = rng.Next(-20, 55),
    //             Summary = Summaries[rng.Next(Summaries.Length)]
    //         })
    //         .ToArray();
    //     }
    // }
}
