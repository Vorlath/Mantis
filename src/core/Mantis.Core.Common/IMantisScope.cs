using Mantis.Core.Common.Interfaces;

namespace Mantis.Core.Common
{
    public interface IMantisScope : IServiceResolver
    {
        IMantisRoot Root { get; }
    }
}