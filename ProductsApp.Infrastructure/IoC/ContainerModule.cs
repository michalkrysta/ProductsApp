using Autofac;
using Microsoft.Extensions.Configuration;
using ProductsApp.Infrastructure.IoC.Modules;

namespace ProductsApp.Infrastructure.IoC
{
    public class ContainerModule : Module
    {
        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
        }
    }
}