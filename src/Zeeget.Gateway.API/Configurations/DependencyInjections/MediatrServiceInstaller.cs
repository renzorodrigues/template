using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Gateway.API.Modules.Authentication.Handlers;

namespace Zeeget.Gateway.API.Configurations.DependencyInjections
{
    public class MediatrServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining(typeof(Program)));
            services.AddMediatR(m =>
                m.RegisterServicesFromAssemblyContaining(typeof(RegisterUserHandler))
            );
            services.AddMediatR(m =>
                m.RegisterServicesFromAssemblyContaining(typeof(LoginUserHandler))
            );
        }
    }
}
