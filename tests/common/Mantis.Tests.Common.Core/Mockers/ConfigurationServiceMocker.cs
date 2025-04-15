using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Services;

namespace Mantis.Tests.Common.Core.Mockers
{
    public class ConfigurationServiceMocker : MockBuilder<ConfigurationService>
    {
        public Mocker<ILifetimeScope> LifetimeScopeMocker { get; } = new Mocker<ILifetimeScope>();
        public List<Configurator> Configurators { get; } = [];

        public ConfigurationService ConfigurationService => this.GetInstance();

        protected override ConfigurationService Build()
        {
            return new ConfigurationService(
                scope: this.LifetimeScopeMocker.Object,
                configurators: this.Configurators);
        }

        public static ConfigurationServiceMocker Create(params Configurator[] configurators)
        {
            ConfigurationServiceMocker mocker = new ConfigurationServiceMocker();
            mocker.Configurators.AddRange(configurators);

            return mocker;
        }
    }
}