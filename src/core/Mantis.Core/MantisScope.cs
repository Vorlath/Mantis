using Autofac;
using Mantis.Core.Common;

namespace Mantis.Core
{
    public class MantisScope(ILifetimeScope autofac, IMantisRoot root) : IMantisScope
    {
        private readonly ILifetimeScope _autofac = autofac;
        public IMantisRoot Root { get; } = root;

        public T Resolve<T>()
            where T : class
        {
            return this._autofac.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return this._autofac.Resolve(type);
        }
    }
}
