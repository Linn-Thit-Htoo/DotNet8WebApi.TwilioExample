using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.TwilioExample.Features.Auth.Register
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterEndpoint : BaseController
    {
        private readonly IMediator _mediator;

        public RegisterEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest, CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(registerRequest);
            var result = await _mediator.Send(command, cancellationToken);

            return Content(result);
        }
    }
}
