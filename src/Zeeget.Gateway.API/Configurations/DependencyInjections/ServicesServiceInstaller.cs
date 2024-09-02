using FluentValidation;
using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Gateway.API.Configurations.Settings;
using Zeeget.Gateway.API.Modules.Authentication.Validators;
using Zeeget.Shared.Services.HttpClient;
using Zeeget.Shared.Services.HttpClient.Interfaces;
using Zeeget.Shared.Services.Logging;

namespace Zeeget.Gateway.API.Configurations.DependencyInjections
{
    public class ServicesServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IHttpClient<,>), typeof(HttpClientFactory<,>));
            services.AddSingleton<ILoggingService, LoggingService>();

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
