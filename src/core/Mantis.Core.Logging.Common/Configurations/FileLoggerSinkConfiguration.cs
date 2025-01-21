using Mantis.Core.Logging.Common.Constants;

namespace Mantis.Core.Logging.Common.Configurations
{
    public class FileLoggerSinkConfiguration
    {
        public bool Enabled { get; set; } = false;
        public string OutputTemplate { get; set; } = LoggingConstants.DefaultOutputTemplate;
        public string? Path { get; set; } = null;
    }
}