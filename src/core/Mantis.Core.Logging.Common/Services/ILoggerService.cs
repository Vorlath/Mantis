namespace Mantis.Core.Logging.Common.Services
{
    public interface ILoggerService
    {
        ILogger GetLogger(Type context);

        ILogger<TContext> GetLogger<TContext>();
    }
}
