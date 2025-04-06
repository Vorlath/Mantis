using System.ComponentModel;

namespace Mantis.Core.Common.Providers
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IMantisVariableProvider<TVariable>
        where TVariable : IMantisVariable
    {
        bool TryGet<T>(out T? variable)
            where T : TVariable;

        IEnumerable<T> GetAll<T>()
            where T : TVariable;
    }
}
