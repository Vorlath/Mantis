using Autofac;
using Mantis.Core.Logging.Common;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;
using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common
{
    public abstract class Scene(ISystemService systemService, ILogger<Scene> logger) : IScene
    {
        protected readonly ISystemService systemService = systemService;
        protected readonly ILogger<Scene> logger = logger;

        protected virtual void Initialize(ILifetimeScope scope)
        {
            foreach (IInitializeSystem initializeSystem in this.systemService.GetSystems<IInitializeSystem>())
            {
                initializeSystem.Initialize(scope);
            }
        }

        protected virtual void Update(GameTime gameTime)
        {
            foreach (IUpdateSystem updateSystem in this.systemService.GetSystems<IUpdateSystem>())
            {
                updateSystem.Update(gameTime);
            }
        }

        protected virtual void Draw(GameTime gameTime)
        {
            foreach (IDrawSystem drawSystem in this.systemService.GetSystems<IDrawSystem>())
            {
                drawSystem.Draw(gameTime);
            }
        }

        void IScene.Initialize(ILifetimeScope scope)
        {
            this.Initialize(scope);
        }

        void IScene.Update(GameTime gameTime)
        {
            this.Update(gameTime);
        }

        void IScene.Draw(GameTime gameTime)
        {
            this.Draw(gameTime);
        }
    }
}
