using Autofac;
using ProductsApp.Infrastructure.Commands;
using IntrospectionExtensions = System.Reflection.IntrospectionExtensions;

namespace ProductsApp.Infrastructure.IoC.Modules
{
    public class CommandModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(CommandModule))
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}