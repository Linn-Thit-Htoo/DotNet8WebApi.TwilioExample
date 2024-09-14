using DotNet8WebApi.TwilioExample.Entities;

namespace DotNet8WebApi.TwilioExample.Services
{
    public interface ISetupService
    {
        Task<Result<Tbl_Setup>> ExpireOtp(string setupId, CancellationToken cancellationToken);
    }
}
