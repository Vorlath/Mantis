using System.ComponentModel;

namespace Mantis.Core.Common
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IMantisVariable
    {

    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IMantisVariable<TValue> : IMantisVariable
    {
        TValue Value { get; }
    }
}