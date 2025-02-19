using Autofac;
using Mantis.Core.Common;

namespace Mantis.Core
{
    public class MantisScope(ILifetimeScope autofac, MantisRoot root) : IMantisScope, IDisposable
    {
        private readonly MantisRoot _root = root;
        private readonly ILifetimeScope _autofac = autofac;
        public IMantisRoot Root => this._root;

        public void Dispose()
        {
            // Alert the root that the current scope has been dispoed
            this._root.RemoveScope(this);
            GC.SuppressFinalize(this);
        }

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
