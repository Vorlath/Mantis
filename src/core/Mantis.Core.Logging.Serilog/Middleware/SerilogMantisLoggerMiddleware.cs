using Autofac;
using Autofac.Core.Resolving.Pipeline;
using Mantis.Core.Logging.Common;

namespace Mantis.Core.Logging.Serilog.Middleware
{
    public sealed class SerilogMantisLoggerMiddleware(Type context) : IResolveMiddleware
    {
        private readonly Type _context = context;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            object instance = context.Resolve(typeof(ILogger<>).MakeGenericType(this._context));
            if (instance is ILogger logger)
            {
                context.ChangeParameters([TypedParameter.From(logger), .. context.Parameters]);
            }

            next(context);
        }
    }
}