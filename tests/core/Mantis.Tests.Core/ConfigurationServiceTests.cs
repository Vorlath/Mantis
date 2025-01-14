using Autofac.Extras.Moq;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Common.Services;
using Mantis.Core.Services;
using Mantis.Tests.Core.Configurations;

namespace Mantis.Tests.Core
{
    public class ConfigurationServiceTests
    {
        [Fact]
        public void ConfigurationService_Configure_NoCast()
        {
            const string TestConfigurationOneName = "Hello World";
            using AutoMock autoMock = AutoMock.GetLoose(builder =>
            {
                // Register TestConfigurationOne configurator
                builder.Configure<TestConfigurationOne>(conf => conf.Name = TestConfigurationOneName);
            });

            IConfigurationService configurationService = autoMock.Create<ConfigurationService>();

            // Resolve TestConfigurationOne configuration
            TestConfigurationOne configuration = configurationService.Configure<TestConfigurationOne>();

            // Ensure the name has been configured as expected
            Assert.Equal(TestConfigurationOneName, configuration.Name);
        }

        [Fact]
        public void ConfigurationService_Configure_Upcast_TestConfigurationOne_To_TestConfigurationBase()
        {
            const int TestConfigurationBaseId = 1337;
            using AutoMock autoMock = AutoMock.GetLoose(builder =>
            {
                // Register TestConfigurationBase configurator
                builder.Configure<TestConfigurationBase>(conf => conf.Id = TestConfigurationBaseId);
            });

            IConfigurationService configurationService = autoMock.Create<ConfigurationService>();

            // Resolve TestConfigurationOne configuration
            TestConfigurationOne configuration = configurationService.Configure<TestConfigurationOne>();

            // Ensure the id has been configured as expected
            Assert.Equal(TestConfigurationBaseId, configuration.Id);
        }

        [Fact]
        public void ConfigurationService_Configure_Ignores_Uncastable_Configurators()
        {
            const string TestConfigurationTwoName = "Hello World";
            using AutoMock autoMock = AutoMock.GetLoose(builder =>
            {
                // Register TestConfigurationTwo configurator
                builder.Configure<TestConfigurationTwo>(conf => conf.Name = TestConfigurationTwoName);
            });

            IConfigurationService configurationService = autoMock.Create<ConfigurationService>();

            // Resolve TestConfigurationOne configuration
            TestConfigurationOne configuration = configurationService.Configure<TestConfigurationOne>();

            // Ensure the id has _not_ been configured as expected
            Assert.NotEqual(TestConfigurationTwoName, configuration.Name);
        }
    }
}