using Autofac;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Logging.Common.Configurations;
using Mantis.Core.Logging.Common.Constants;

namespace Mantis.Core.Logging.Common.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder ConfigureConsoleLoggerSink(
            this ContainerBuilder builder,
            string outputTemplate = LoggingConstants.DefaultOutputTemplate,
            bool enabled = true)
        {
            return builder.Configure<ConsoleLoggerSinkConfiguration>(conf =>
            {
                conf.OutputTemplate = outputTemplate;
                conf.Enabled = enabled;
            });
        }

        public static ContainerBuilder ConfigureFileLoggerSink(
            this ContainerBuilder builder,
            string path,
            string outputTemplate = LoggingConstants.DefaultOutputTemplate,
            bool enabled = true)
        {
            return builder.Configure<FileLoggerSinkConfiguration>(conf =>
            {
                conf.Path = path;
                conf.OutputTemplate = outputTemplate;
                conf.Enabled = enabled;
            });
        }
    }
}
