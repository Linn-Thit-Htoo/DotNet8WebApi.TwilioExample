using DotNet8WebApi.TwilioExample.Entities;
using MediatR;

namespace DotNet8WebApi.TwilioExample.Features.Setup.ExpireSetup
{
    public class ExpireSetupCommand : IRequest<Result<SetupDTO>>
    {
        public string SetupId { get; set; }

        public ExpireSetupCommand(string setupId)
        {
            SetupId = setupId;
        }
    }
}
