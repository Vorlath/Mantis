using Mantis.Core.Common;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;

namespace Mantis.Engine.Services
{
    public sealed class SceneService(IMantisRoot root) : ISceneService
    {
        private readonly IMantisRoot _root = root;
        private readonly List<IScene> _scenes = [];

        public TScene Create<TScene>()
            where TScene : class, IScene
        {
            TScene scene = this._root.CreateScope().Resolve<TScene>();

            // Initialize the new scene
            scene.Initialize();
            this._scenes.Add(scene);

            return scene;
        }

        public IEnumerable<IScene> GetAll()
        {
            return this._scenes;
        }
    }
}