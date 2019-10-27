using Autofac;
using ProductsApp.Infrastructure.Services;
using IntrospectionExtensions = System.Reflection.IntrospectionExtensions;

namespace ProductsApp.Infrastructure.IoC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ServiceModule))
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}