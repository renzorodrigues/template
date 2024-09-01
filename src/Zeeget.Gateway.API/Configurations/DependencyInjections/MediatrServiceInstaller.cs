using MediatR;
using Zeeget.Gateway.API.Configurations.DependencyInjections.Base;
using Zeeget.Gateway.API.Modules.Authentication.Handlers;
using Zeeget.Shared.Services.Logging;
using Zeeget.Shared.Utils.Validations;

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

            // Registrar comportamentos do pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // Registrar serviços de logging
            services.AddSingleton<ILoggingService, LoggingService>();
        }
    }
}
