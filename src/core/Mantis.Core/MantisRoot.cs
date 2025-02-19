using System.Collections.ObjectModel;
using Autofac;
using Mantis.Core.Builders;
using Mantis.Core.Common;
using Mantis.Core.Common.Builders;

namespace Mantis.Core
{
    public class MantisRoot : IMantisRoot, IDisposable
    {
        private readonly ILifetimeScope _autofac;
        private readonly List<IMantisScope> _scopes;

        public IMantisScope MainScope { get; }
        public ReadOnlyCollection<IMantisScope> Scopes { get; }

        public MantisRoot(ILifetimeScope autofac)
        {
            this._autofac = autofac;
            this._scopes = [];

            this.Scopes = new ReadOnlyCollection<IMantisScope>(this._scopes);

            this.MainScope = new MantisScope(autofac, this);
            this._scopes.Add(this.MainScope);
        }

        public IMantisScope CreateScope(Action<IMantisScopeBuilder>? build = null)
        {
            ILifetimeScope autofacScope = this._autofac.BeginLifetimeScope(containerBuilder =>
            {
                MantisScopeBuilder mantisScopeBuilder = new MantisScopeBuilder(containerBuilder, this);
                build?.Invoke(mantisScopeBuilder);
            });

            IMantisScope mantisScope = autofacScope.Resolve<IMantisScope>();

            return mantisScope;
        }

        public void RemoveScope(MantisScope scope)
        {
            this._scopes.Remove(scope);
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

        public void Dispose()
        {
            this._autofac.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}