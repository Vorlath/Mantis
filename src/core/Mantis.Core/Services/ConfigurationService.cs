using System.Runtime.InteropServices;
using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Common.Services;

namespace Mantis.Core.Services
{
    public class ConfigurationService(ILifetimeScope scope, IEnumerable<Configurator> configurators) : IConfigurationService
    {
        private readonly ILifetimeScope _scope;
        private readonly Configurator[] _configurators = [.. configurators];
        private readonly Dictionary<Type, Configurator[]> _configuratorsByType = [];

        public void Configure<T>(T instance)
            where T : class
        {
            ref Configurator[]? configurators = ref CollectionsMarshal.GetValueRefOrAddDefault(this._configuratorsByType, typeof(T), out bool exists);
            if (exists == false)
            {
                configurators = [.. this._configurators.Where(c => c.TryConfigureInstance(this._scope, instance))];
                return;
            }

            foreach (Configurator configurator in configurators!)
            {
                configurator.TryConfigureInstance(this._scope, instance);
            }
        }
    }
}