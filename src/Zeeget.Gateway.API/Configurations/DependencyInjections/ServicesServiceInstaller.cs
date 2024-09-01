﻿using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Gateway.API.Configurations.Settings;
using Zeeget.Shared.Services.HttpClient;
using Zeeget.Shared.Services.HttpClient.Interfaces;

namespace Zeeget.Gateway.API.Configurations.DependencyInjections
{
    public class ServicesServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IHttpClient<,>), typeof(HttpClientFactory<,>));

            var httpClientSettings = new HttpClientSettings();
            configuration.Bind("HttpClientSettings", httpClientSettings);
            services.AddSingleton(httpClientSettings);
        }
    }
}
