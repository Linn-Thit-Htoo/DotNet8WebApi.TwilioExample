using DotNet8WebApi.TwilioExample.Db;
using DotNet8WebApi.TwilioExample.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DotNet8WebApi.TwilioExample.Features.Setup.ExpireSetup
{
    public class ExpireSetupCommandHandler : IRequestHandler<ExpireSetupCommand, Result<SetupDTO>>
    {
        private readonly AppDbContext _context;

        public ExpireSetupCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<SetupDTO>> Handle(ExpireSetupCommand request, CancellationToken cancellationToken)
        {
            Result<SetupDTO> result;
            try
            {
                var setup = await _context.Tbl_Setups.FindAsync(request.SetupId, cancellationToken);
                if (setup is null)
                {
                    result = Result<SetupDTO>.NotFound();
                    goto result;
                }

                _context.Tbl_Setups.Remove(setup);
                await _context.SaveChangesAsync(cancellationToken);

                result = Result<SetupDTO>.Success();
            }
            catch (Exception ex)
            {
                result = Result<SetupDTO>.Fail(ex);
            }

        result:
            return result;
        }
    }
}
