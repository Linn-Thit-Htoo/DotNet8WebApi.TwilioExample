using DotNet8WebApi.TwilioExample.Db;
using DotNet8WebApi.TwilioExample.Entities;

namespace DotNet8WebApi.TwilioExample.Services
{
    public class SetupService : ISetupService
    {
        private readonly AppDbContext _context;

        public SetupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Tbl_Setup>> ExpireOtp(string setupId, CancellationToken cancellationToken)
        {
            Result<Tbl_Setup> result;
            try
            {
                var setup = await _context.Tbl_Setups.FindAsync([setupId, cancellationToken], cancellationToken: cancellationToken);
                if (setup is null)
                {
                    result = Result<Tbl_Setup>.NotFound();
                    goto result;
                }

                _context.Tbl_Setups.Remove(setup);
                await _context.SaveChangesAsync(cancellationToken);

                result = Result<Tbl_Setup>.Success();
            }
            catch (Exception ex)
            {
                result = Result<Tbl_Setup>.Fail(ex);
            }

        result:
            return result;
        }
    }
}
