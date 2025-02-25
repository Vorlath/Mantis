using System.Collections.ObjectModel;
using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Interfaces;

namespace Mantis.Core.Common
{
    public interface IMantisRoot : IServiceResolver
    {
        /// <summary>
        /// There must be a primary <see cref="IMantisScope"/> to represent the root and
        /// all singletons. This is that scope.
        /// </summary>
        IMantisScope MainScope { get; }

        /// <summary>
        /// All active <see cref="IMantisScope"/>s, including the current <see cref="MainScope"/>
        /// </summary>
        ReadOnlyCollection<IMantisScope> Scopes { get; }

        /// <summary>
        /// Create a new <see cref="IMantisScope"/> instance.
        /// </summary>
        /// <returns></returns>
        IMantisScope CreateScope(Action<IMantisScopeBuilder>? build = null);
    }
}