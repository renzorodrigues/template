using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak;

namespace Zeeget.Gateway.API.Configurations.DependencyInjections
{
    public class ServicesServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IKeycloakService, KeycloakService>();
        }
    }
}
