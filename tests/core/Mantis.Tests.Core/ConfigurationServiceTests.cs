using Mantis.Tests.Core.Configurations;
using Mantis.Tests.Core.Fixtures;
using Mantis.Tests.Core.Stubs;

namespace Mantis.Tests.Core
{
    public class ConfigurationServiceTests
    {
        public const int TestConfigurationId = 1337;
        public const string TestConfigurationName = "Hello World";

        [Fact]
        public void RegisterClassConfigurationResolveClassConfiguration_IsConfigured()
        {
            ConfigurationServiceFixture.RegisterConfiguratorThenResolveAndVerifyConfiguration<TestConfigurationClassA, TestConfigurationClassA>(
                configure: conf => conf.Name = TestConfigurationName,
                verify: conf => conf.Name == TestConfigurationName);
        }

        [Fact]
        public void RegistorInterfaceConfigurationResolveClassConfiguration_IsConfigured()
        {
            ConfigurationServiceFixture.RegisterConfiguratorThenResolveAndVerifyConfiguration<ITestConfigurationInterface, TestConfigurationClassA>(
                configure: conf => conf.Id = TestConfigurationId,
                verify: conf => conf.Id == TestConfigurationId);
        }

        [Fact]
        public void RegisterClassAConfigurationResolveClassBConfiguration_IsNotConfigured()
        {
            ConfigurationServiceFixture.RegisterConfiguratorThenResolveAndVerifyConfiguration<TestConfigurationClassA, TestConfigurationClassB>(
                configure: conf => conf.Name = TestConfigurationName,
                verify: conf => conf.Name != TestConfigurationName);
        }
    }
}