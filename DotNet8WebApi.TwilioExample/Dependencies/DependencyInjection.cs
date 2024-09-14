using DotNet8WebApi.TwilioExample.Db;
using DotNet8WebApi.TwilioExample.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace DotNet8WebApi.TwilioExample.Dependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            return services.AddDbContextService(builder).AddServices().AddHangfireService(builder).AddMediatRService();
        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ISetupService, SetupService>();
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

        private static IServiceCollection AddMediatRService(this IServiceCollection services)
        {
            return services.AddMediatR(cf =>
                cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
            );
        }
    }
}
