namespace Zeeget.Gateway.API.Configurations.DependencyInjections.Base
{
    public interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
