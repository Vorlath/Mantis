namespace Mantis.Core.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterCoreServices<T>(this ContainerBuilder builder)
            where T : class, new()
        {
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Configuration<>)).As(typeof(IConfiguration<>)).InstancePerDependency();

            return builder;
        }
    }
}