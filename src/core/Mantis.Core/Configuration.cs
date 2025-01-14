using Mantis.Core.Common;
using Mantis.Core.Common.Services;

namespace Mantis.Core
{
    public class Configuration<T>(IConfigurationService configurationService) : IConfiguration<T>
        where T : class, new()
    {
        public T Value { get; set; } = configurationService.Configure<T>();
    }
}