using Autofac;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;

namespace Mantis.Engine.Services
{
    public sealed class SceneService(ILifetimeScope scope) : ISceneService
    {
        private readonly ILifetimeScope _scope = scope;
        private readonly List<IScene> _scenes = [];

        public TScene Create<TScene>()
            where TScene : IScene
        {
            // TODO: Create a new scsope per scene
            TScene scene = this._scope.Resolve<TScene>();
            this._scenes.Add(scene);
            scene.Initialize(this._scope);
            return scene;
        }

        public IEnumerable<IScene> GetAll()
        {
            return this._scenes;
        }
    }
}