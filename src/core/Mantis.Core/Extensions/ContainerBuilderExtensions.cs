using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Common.Services;
using Mantis.Core.Services;

namespace Mantis.Core.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterCoreServices(this ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Configuration<>)).As(typeof(IConfiguration<>)).InstancePerDependency();

            return builder;
        }
    }
}