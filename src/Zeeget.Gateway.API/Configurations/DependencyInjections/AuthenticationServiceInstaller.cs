using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Gateway.API.Configurations.Settings.Modules.Authentication;
using Zeeget.Shared.Utilities.Guard;

namespace Zeeget.Gateway.API.Configurations.DependencyInjections
{
    public class AuthenticationServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var authSettings = ConfigurationGuard.EnsureConfiguration(
                services
                    .BuildServiceProvider()
                    .GetService<IOptions<AuthenticationSettings>>()
                    ?.Value,
                nameof(AuthenticationSettings)
            );

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = authSettings.Authority;
                    options.Audience = authSettings.Audience;
                    options.RequireHttpsMetadata = authSettings.RequireHttpsMetadata;
                });

            services.AddAuthorization();
        }
    }
}
