using Mantis.Core.Common;
using Mantis.Core.Common.Services;

namespace Mantis.Core.Services
{
    public class EnvironmentVariableService(IEnumerable<IEnvironmentVariable> variables) :
        MantisVariableService<IEnvironmentVariable>(variables),
        IEnvironmentVariableService
    {
    }
}
