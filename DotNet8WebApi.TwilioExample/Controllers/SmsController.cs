using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DotNet8WebApi.TwilioExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SmsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> SendSms()
        {
            var accountSid = "";
            var authToken = "";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
              new PhoneNumber("+959773871112"));
            messageOptions.From = new PhoneNumber("+12073062974");
            messageOptions.Body = "Hello from Twilio";


            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);

            return Ok(message);
        }
    }
}
