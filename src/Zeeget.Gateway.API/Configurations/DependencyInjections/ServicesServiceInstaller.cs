using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Shared.Commons.Configurations.Settings.HttpClient;
using Zeeget.Shared.Commons.Services.HttpClient;
using Zeeget.Shared.Commons.Services.HttpClient.Interfaces;
using Zeeget.Shared.Commons.Utils.Validations;

namespace Zeeget.Gateway.API.Configurations.DependencyInjections
{
    public class ServicesServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IHttpClient<,>), typeof(HttpClientFactory<,>));

            services.AddTransient<IValidator, ValidationRequest>();

            var httpClientSettings = new HttpClientSettings();
            configuration.Bind("HttpClientSettings", httpClientSettings);
            services.AddSingleton(httpClientSettings);
        }
    }
}
