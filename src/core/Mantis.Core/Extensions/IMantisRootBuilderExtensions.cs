using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Common.Services;
using Mantis.Core.Services;

namespace Mantis.Core.Extensions
{
    public static class IMantisRootBuilderExtensions
    {
        public static IMantisRootBuilder RegisterCoreServices(this IMantisRootBuilder builder)
        {
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Configuration<>)).As(typeof(IConfiguration<>)).InstancePerDependency();

            return builder;
        }
    }
}