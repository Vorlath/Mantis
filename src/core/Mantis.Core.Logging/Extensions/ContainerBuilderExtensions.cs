using Autofac;
using Mantis.Core.Logging.Modules;

namespace Mantis.Core.Logging.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterCoreLoggingServices(this ContainerBuilder builder)
        {
            builder.RegisterModule<LoggingModule>();

            return builder;
        }
    }
}