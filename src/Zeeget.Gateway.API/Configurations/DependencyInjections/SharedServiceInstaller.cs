using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Shared.Api;
using IResult = Zeeget.Shared.Api.IResult;

namespace Zeeget.Gateway.API.Configurations.DependencyInjections
{
    public class SharedServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IResult, Result>();
        }
    }
}
