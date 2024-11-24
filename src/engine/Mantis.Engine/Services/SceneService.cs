using Autofac;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
using System.Runtime.CompilerServices;

namespace Mantis.Engine.Services
{
    public sealed class SceneService : ISceneService
    {
        private ILifetimeScope _scope;
        private List<IScene> _scenes;
        public SceneService(ILifetimeScope scope)
        {
            _scope = scope;
            _scenes = new List<IScene>();
        }

        public TScene Create<TScene>()
            where TScene : IScene
        {
            TScene scene = _scope.Resolve<TScene>();
            _scenes.Add(scene);
            return scene;
        }

        public IEnumerable<IScene> GetAll()
        {
            return _scenes;
        }
    }
}
