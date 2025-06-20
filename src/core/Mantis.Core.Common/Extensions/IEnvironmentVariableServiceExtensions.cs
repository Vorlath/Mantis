using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common.Constants;
using Mantis.Core.Common.Services;

namespace Mantis.Core.Common.Extensions
{
    public static class IEnvironmentVariableServiceExtensions
    {
        public static string GetCompany(this IEnvironmentVariableService environmentVariableService)
        {
            return environmentVariableService.Get<MantisCoreVariables.Environment.Company>().Value;
        }

        public static string GetProject(this IEnvironmentVariableService environmentVariableService)
        {
            return environmentVariableService.Get<MantisCoreVariables.Environment.Project>().Value;
        }
    }
}