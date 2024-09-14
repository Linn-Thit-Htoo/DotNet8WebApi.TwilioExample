﻿using DotNet8WebApi.TwilioExample.Services.SmsService;
using Hangfire;

namespace DotNet8WebApi.TwilioExample.Dependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            return services.AddServices().AddHangfireService(builder);
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ISmsService, SmsService>();
        }

        private static IServiceCollection AddHangfireService(
            this IServiceCollection services,
            WebApplicationBuilder builder
        )
        {
            builder.Services.AddHangfire(opt =>
            {
                opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("DbConnection"))
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings();
            });

            builder.Services.AddHangfireServer();
            return services;
        }
    }
}