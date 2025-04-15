using Mantis.Tests.Core.Stubs;

namespace Mantis.Tests.Core.Configurations
{
    public class TestConfigurationClassB : ITestConfigurationInterface
    {
        public int Id { get; set; }

        public string Name = string.Empty;
        public int Age = 0;


    }
}