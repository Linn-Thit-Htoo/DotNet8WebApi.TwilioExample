using DotNet8WebApi.TwilioExample.Db;
using DotNet8WebApi.TwilioExample.Entities;
using DotNet8WebApi.TwilioExample.Extensions;
using DotNet8WebApi.TwilioExample.Services;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DotNet8WebApi.TwilioExample.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthDTO>>
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public RegisterCommandHandler(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

                BackgroundJob.Schedule<ISetupService>(x => x.ExpireOtp(setup.SetupId, cancellationToken), TimeSpan.FromMinutes(1));

                SendOtpViaSms(user.PhoneNumber);

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                result = Result<AuthDTO>.Success();
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

        private void SendOtpViaSms(string phone)
        {
            var accountSid = _configuration.GetSection("AccountSid").Value!;
            var authToken = _configuration.GetSection("AuthToken").Value!;
            TwilioClient.Init(accountSid, authToken);

            //var messageOptions = new CreateMessageOptions(
            //  new PhoneNumber("+959773871112"));
            var messageOptions = new CreateMessageOptions(
              new PhoneNumber(phone));
            messageOptions.From = new PhoneNumber("+12073062974");
            messageOptions.Body = "Hello from Twilio";


            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }
    }
}
