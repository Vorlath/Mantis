using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;
using Microsoft.Xna.Framework;

namespace Mantis.Engine.Systems
{
    public class SceneFrameSystem(ISceneService sceneService) : IGlobalSystem, IDrawSystem, IUpdateSystem
    {
        private readonly ISceneService _sceneService = sceneService;

        [SequenceGroup<UpdateSequenceGroupEnum>(UpdateSequenceGroupEnum.Update)]
        public void Update(GameTime gameTime)
        {
            foreach (IScene scene in this._sceneService.GetAll())
            {
                scene.Update(gameTime);
            }
        }

        [SequenceGroup<DrawSequenceGroupEnum>(DrawSequenceGroupEnum.Draw)]
        public void Draw(GameTime gameTime)
        {
            foreach (IScene scene in this._sceneService.GetAll())
            {
                scene.Draw(gameTime);
            }
        }
    }
}