using Mantis.Core.Logging.Common.Constants;

namespace Mantis.Core.Logging.Common.Configurations
{
    public class ConsoleLoggerSinkConfiguration
    {
        public bool Enabled { get; set; } = false;
        public string OutputTemplate { get; set; } = LoggingConstants.DefaultOutputTemplate;
    }
}