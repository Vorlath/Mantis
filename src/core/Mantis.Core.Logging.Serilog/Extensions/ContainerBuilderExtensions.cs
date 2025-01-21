using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Logging.Common;
using Mantis.Core.Logging.Common.Configurations;
using Mantis.Core.Logging.Common.Enums;
using Serilog;

namespace Mantis.Core.Logging.Serilog.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterSerilogLoggingServices(this ContainerBuilder builder, LogLevelEnum minimumLogLevel)
        {
            builder.RegisterGeneric(typeof(SerilogMantisLogger<>)).As(typeof(ILogger<>)).InstancePerLifetimeScope();
            builder.Configure<LoggerConfiguration>((scope, serilogLoggerConfiguration) =>
            {
                // Configure the minimum log level
                serilogLoggerConfiguration.MinimumLevel.Is(minimumLogLevel.ToLogEventLevel());

                // Register console sink
                IConfiguration<ConsoleLoggerSinkConfiguration> consoleSinkConfiguration = scope.Resolve<IConfiguration<ConsoleLoggerSinkConfiguration>>();
                if (consoleSinkConfiguration.Value.Enabled == true)
                {
                    serilogLoggerConfiguration.WriteTo.Console(outputTemplate: consoleSinkConfiguration.Value.OutputTemplate);
                }

                // Register file sink
                IConfiguration<FileLoggerSinkConfiguration> fileSinkConfiguration = scope.Resolve<IConfiguration<FileLoggerSinkConfiguration>>();
                if (fileSinkConfiguration.Value.Enabled == true)
                {
                    serilogLoggerConfiguration.WriteTo.File(
                        path: fileSinkConfiguration.Value.Path ?? throw new ArgumentNullException(),
                        outputTemplate: fileSinkConfiguration.Value.OutputTemplate,
                        retainedFileCountLimit: 8,
                        shared: true
                    );
                }
            });

            return builder;
        }
    }
}