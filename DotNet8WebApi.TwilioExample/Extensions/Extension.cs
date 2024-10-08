﻿using DotNet8WebApi.TwilioExample.Entities;
using DotNet8WebApi.TwilioExample.Features.Auth.Register;

namespace DotNet8WebApi.TwilioExample.Extensions
{
    public static class Extension
    {
        public static Tbl_User ToEntity(this RegisterRequestDTO registerRequest)
        {
            return new Tbl_User
            {
                UserId = Ulid.NewUlid().ToString(),
                UserName = registerRequest.UserName,
                PhoneNumber = registerRequest.PhoneNumber,
                Password = registerRequest.Password,
                DeleteFlag = false
            };
        }
    }
}
