using Mantis.Core.Common.Implementations;

namespace Mantis.Core.Common.Constants
{
    public class MantisCoreVariables
    {
        public static class Environment
        {
            public class Company(string value) : EnvironmentVariable<Company, string>(value) { }
            public class Project(string value) : EnvironmentVariable<Project, string>(value) { }
        }
    }
}
