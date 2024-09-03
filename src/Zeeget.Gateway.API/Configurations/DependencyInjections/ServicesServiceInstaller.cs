using FluentValidation;
using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Gateway.API.Configurations.Settings;
using Zeeget.Gateway.API.Modules.Authentication.Validators;
using Zeeget.Shared.Services.HttpRequest;
using Zeeget.Shared.Services.HttpRequest.Interfaces;
using Zeeget.Shared.Services.Logging;

namespace Zeeget.Gateway.API.Configurations.DependencyInjections
{
    public class ServicesServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILoggingService, LoggingService>();

            services.AddScoped<IHttpService, HttpService>();

            RegisterSettings(services, configuration);
            RegisterValidators(services);
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<LoginUserQueryValidator>();
        }

        private static void RegisterSettings(
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            var httpClientSettings = new HttpClientSettings();
            configuration.Bind("HttpClientSettings", httpClientSettings);
            services.AddSingleton(httpClientSettings);
        }
    }
}
