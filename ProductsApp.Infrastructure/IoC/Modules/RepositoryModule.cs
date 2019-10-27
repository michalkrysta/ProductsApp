using System.Reflection;
using Autofac;
using ProductsApp.Core.Repositories;
using ProductsApp.Infrastructure.Services;
using Module = Autofac.Module;

namespace ProductsApp.Infrastructure.IoC.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}