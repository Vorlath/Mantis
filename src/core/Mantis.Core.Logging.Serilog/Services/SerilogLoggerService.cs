using System.Diagnostics;
using System.Runtime.InteropServices;
using Mantis.Core.Common;
using Mantis.Core.Logging.Common;
using Mantis.Core.Logging.Common.Services;
using Serilog;
using Serilog.Events;
using ILogger = Mantis.Core.Logging.Common.ILogger;

namespace Mantis.Core.Logging.Serilog.Services
{
    public class SerilogLoggerService(IConfiguration<LoggerConfiguration> configuration) : ILoggerService
    {
        private readonly Dictionary<Type, ILogger> _cache = [];
        private readonly IConfiguration<LoggerConfiguration> _configuration = configuration;
        private readonly ISerilogLogger _base = configuration.Value
            .MinimumLevel.Is(LogEventLevel.Verbose)
            .CreateLogger();

        public ILogger GetLogger(Type context)
        {
            ref ILogger? logger = ref CollectionsMarshal.GetValueRefOrAddDefault(this._cache, context, out bool exists);
            if (exists == false)
            {
                // If the logger cache does not contain an instance we must create one using reflection

                // We make a generic type of SerilogMantisLogger<context>
                Type loggerType = typeof(SerilogMantisLogger<>).MakeGenericType(context);

                // Using Activactor.CreateInstance we can, at runtime, create an instance of the Type object
                // Notice we are passing in this._configuration through to the constructor
                // This line is equivilent of saying: new SerilogMantisLogger<context>(this._configuration);
                logger = (ILogger)(Activator.CreateInstance(loggerType, this._base) ?? throw new NotImplementedException());
            }

            // If we get this far we know logger has a value
            return logger!;
        }

        public ILogger<TContext> GetLogger<TContext>()
        {
            ref ILogger? logger = ref CollectionsMarshal.GetValueRefOrAddDefault(this._cache, typeof(TContext), out bool exists);
            if (exists == false)
            {
                // If the logger cache does not contain an instance we must create one
                logger = new SerilogMantisLogger<TContext>(this._base);
            }

            if (logger is not ILogger<TContext> casted)
            {
                // This should never be possible. Somehow a logger has been cached
                // with the incorrect context type key
                throw new UnreachableException();
            }

            return casted;
        }
    }
}