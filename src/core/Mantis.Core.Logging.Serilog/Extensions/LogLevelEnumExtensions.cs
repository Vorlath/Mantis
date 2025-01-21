using System.Diagnostics;
using Mantis.Core.Logging.Common.Enums;
using Serilog.Events;

namespace Mantis.Core.Logging.Serilog.Extensions
{
    public static class LogLevelEnumExtensions
    {
        public static LogEventLevel ToLogEventLevel(this LogLevelEnum logLevel)
        {
            return logLevel switch
            {
                LogLevelEnum.Verbose => LogEventLevel.Verbose,
                LogLevelEnum.Debug => LogEventLevel.Debug,
                LogLevelEnum.Information => LogEventLevel.Information,
                LogLevelEnum.Warning => LogEventLevel.Warning,
                LogLevelEnum.Error => LogEventLevel.Error,
                LogLevelEnum.Fatal => LogEventLevel.Fatal,
                _ => throw new UnreachableException()
            };
        }
    }
}