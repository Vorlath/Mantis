using Mantis.Core.Common;
using Mantis.Tests.Common.Core.Mockers;

namespace Mantis.Tests.Core.Fixtures
{
    public class ConfigurationServiceFixture
    {
        public static void RegisterConfiguratorThenResolveAndVerifyConfiguration<TConfigurator, TConfiguration>(
            Action<TConfigurator> configure,
            Func<TConfiguration, bool> verify
        )
            where TConfigurator : class
            where TConfiguration : class, new()
        {
            // Register
            Configurator configurator = new Configurator<TConfigurator>((_, conf) => configure(conf));
            ConfigurationServiceMocker mocker = ConfigurationServiceMocker.Create(configurator);

            // Resolve
            TConfiguration configuration = new TConfiguration();
            mocker.ConfigurationService.Configure(configuration);

            // Verify
            bool result = verify(configuration);
            Assert.True(result);
        }
    }
}