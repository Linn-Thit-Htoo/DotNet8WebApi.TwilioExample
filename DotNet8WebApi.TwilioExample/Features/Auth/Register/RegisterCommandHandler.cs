using DotNet8WebApi.TwilioExample.Db;
using DotNet8WebApi.TwilioExample.Entities;
using MediatR;

namespace DotNet8WebApi.TwilioExample.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthDTO>>
    {
        private readonly AppDbContext _context;

        public RegisterCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public Task<Result<AuthDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
