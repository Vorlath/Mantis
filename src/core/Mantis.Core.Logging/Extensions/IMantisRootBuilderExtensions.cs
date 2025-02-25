using Autofac;
using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Logging.Modules;

namespace Mantis.Core.Logging.Extensions
{
    public static class IMantisRootBuilderExtensions
    {
        public static IMantisRootBuilder RegisterCoreLoggingServices(this IMantisRootBuilder builder)
        {
            builder.RegisterModule<LoggingModule>();

            return builder;
        }
    }
}