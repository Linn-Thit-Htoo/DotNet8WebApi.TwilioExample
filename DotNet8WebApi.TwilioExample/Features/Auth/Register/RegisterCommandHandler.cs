using DotNet8WebApi.TwilioExample.Entities;
using MediatR;

namespace DotNet8WebApi.TwilioExample.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthDTO>>
    {
        public Task<Result<AuthDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
