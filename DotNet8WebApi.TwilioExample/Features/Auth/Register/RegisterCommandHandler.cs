using DotNet8WebApi.TwilioExample.Db;
using DotNet8WebApi.TwilioExample.Entities;
using DotNet8WebApi.TwilioExample.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.TwilioExample.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthDTO>>
    {
        private readonly AppDbContext _context;

        public RegisterCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<AuthDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Result<AuthDTO> result;
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                bool phoneDuplicate = await _context.Tbl_Users.AnyAsync(x => x.PhoneNumber == request.RegisterRequest.PhoneNumber && !x.DeleteFlag, cancellationToken: cancellationToken);
                if (phoneDuplicate)
                {
                    result = Result<AuthDTO>.Duplicate("Phone Number already exists.");
                    goto result;
                }

                var user = request.RegisterRequest.ToEntity();
                await _context.Tbl_Users.AddAsync(user, cancellationToken);

                string eightDigit = GetEightDigitRandomNumber();
                var setup = new Tbl_Setup()
                {
                    SetupId = Ulid.NewUlid().ToString(),
                    UserId = user.UserId,
                    OtpCode = eightDigit,
                    CreatedDate = DateTime.Now,
                };
                await _context.Tbl_Setups.AddAsync(setup, cancellationToken);


            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                result = Result<AuthDTO>.Fail(ex);
            }

        result:
            return result;
        }

        private string GetEightDigitRandomNumber()
        {
            Random r = new();
            int randNum = r.Next(10000000, 100000000); // Generates a number between 10000000 and 99999999
            return randNum.ToString();
        }
    }
}
