using Polly;
using Polly.Extensions.Http;
using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Shared.Services.HttpRequest;
using Zeeget.Shared.Services.HttpRequest.Interfaces;

namespace Zeeget.Gateway.API.Configurations.DependencyInjections
{
    public class PolicyServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpClient("default")
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services
                .AddHttpClient("GetToken")
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services
                .AddHttpClient("PostUser")
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddScoped<IHttpService, HttpService>();
        }

        private IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError() // It handles with errors: 5xx, 408, and 404
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                );
        }

        private IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
