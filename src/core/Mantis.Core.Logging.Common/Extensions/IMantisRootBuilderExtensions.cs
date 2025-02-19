using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Logging.Common.Configurations;
using Mantis.Core.Logging.Common.Constants;

namespace Mantis.Core.Logging.Common.Extensions
{
    public static class IMantisRootBuilderExtensions
    {
        public static IMantisRootBuilder ConfigureConsoleLoggerSink(
            this IMantisRootBuilder builder,
            string outputTemplate = LoggingConstants.DefaultOutputTemplate,
            bool enabled = true)
        {
            return builder.Configure<ConsoleLoggerSinkConfiguration>(conf =>
            {
                conf.OutputTemplate = outputTemplate;
                conf.Enabled = enabled;
            });
        }

        public static IMantisRootBuilder ConfigureFileLoggerSink(
            this IMantisRootBuilder builder,
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