using System.ComponentModel;
using Mantis.Core.Common.Providers;

namespace Mantis.Core.Common.Builders
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IMantisVariableBuilder<TSelf, TVariable> :
        IMantisVariableProvider<TVariable>,
        IEnumerable<TVariable>
            where TSelf : IMantisVariableBuilder<TSelf, TVariable>
            where TVariable : IMantisVariable
    {
        TSelf Add(TVariable variable);
    }
}