using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;
using Microsoft.Xna.Framework;

namespace Mantis.Engine.Systems
{
    public class SceneFrameSystem(ISceneService sceneService) : IGlobalSystem, IDrawSystem, IUpdateSystem
    {
        private readonly ISceneService _sceneService = sceneService;

        public void Update(GameTime gameTime)
        {
            foreach (IScene scene in this._sceneService.GetAll())
            {
                scene.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IScene scene in this._sceneService.GetAll())
            {
                scene.Draw(gameTime);
            }
        }
    }
}