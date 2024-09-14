using DotNet8WebApi.TwilioExample.Entities;
using MediatR;

namespace DotNet8WebApi.TwilioExample.Features.Auth.Register
{
    public class RegisterCommand : IRequest<Result<AuthDTO>>
    {
        public RegisterRequestDTO RegisterRequest { get; set; }
    }
}
