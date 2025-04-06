using System.ComponentModel;
using Mantis.Core.Common.Providers;

namespace Mantis.Core.Common.Services
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IMantisVariableService<TVariable> : IMantisVariableProvider<TVariable>
        where TVariable : IMantisVariable
    {
        T Get<T>()
            where T : TVariable;
        bool Has<T>()
            where T : TVariable;
    }
}