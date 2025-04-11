using Mantis.Core.Logging.Common;
using Mantis.Core.MonoGame.Common;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;
using Mantis.Example.LunarLander.Components;
using Mantis.Example.LunarLander.Descriptors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Mantis.Example.LunarLander.Scenes
{
    public static class ExclusiveGroups
    {

        public static readonly ExclusiveGroup LanderGroup = new();
        public static readonly ExclusiveGroup BlockGroup = new();
        public static readonly ExclusiveGroup WallGroup = new();
        public static readonly ExclusiveGroup PaddleGroup = new();
    }

    public class GameScene : IScene
    {
        private readonly EntitiesSubmissionScheduler _entitiesSubmissionScheduler;
        private readonly ILogger<GameScene> _logger;

        private readonly ISystemService _systemService;
        public GameScene(
            EnginesRoot enginesRoot,
            IEntityFactory entityFactory,
            EntitiesSubmissionScheduler entitiesSubmissionScheduler,
            GraphicsDevice graphics,
            ILogger<GameScene> logger,
            ISystemService systemService,
            ContentManager content)
        {
            this._entitiesSubmissionScheduler = entitiesSubmissionScheduler;
            this._logger = logger;
            this._systemService = systemService;
            foreach (IEngine engine in this._systemService.GetSystems<IEngine>())
            {
                enginesRoot.AddEngine(engine);
            }

            var _texture = content.Load<Texture2D>("LanderSheet");
            var SpriteSheet = new SpriteSheet(_texture, [
                new SpriteData("1", new Rectangle(0, 0, 16, 16)),
                new SpriteData("2", new Rectangle(16, 0, 16, 16)),
                new SpriteData("3", new Rectangle(32, 0, 16, 16)),
                new SpriteData("4", new Rectangle(0, 16, 16, 16)),
                new SpriteData("5", new Rectangle(16, 16, 16, 16))
                ]);

            // Name this
            AnimationType LanderIdle = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("1", 10000)
            ]);

            AnimationType LanderBurnStart = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("2", 100),
                new AnimationFrameContext("3", 500)
            ]);

            AnimationType LanderBurnEnd = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("3", 500),
                new AnimationFrameContext("2", 500),
                new AnimationFrameContext("1", 500)
            ]);

            AnimationType LanderRotateCW = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("4", 500)
            ]);

            AnimationType LanderRotateCCW = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("5", 500)
            ]);

            // lander
            int num = 0;
            var entityInitializer = entityFactory.BuildEntity<LanderDescriptor>(0, ExclusiveGroups.LanderGroup);
            entityInitializer.Init(new Transform2D(0, 0, 270));
            entityInitializer.Init(new Velocity(75, 0));
            entityInitializer.Init(new Gravity(5));
            entityInitializer.Init(new Size(32, 32));
            entityInitializer.Init(new Animated(LanderIdle));
            num++;

            // Example logger usage.
            this._logger.Debug("Created GameScene!");
        }

        public void Draw(GameTime gameTime)
        {
            // Example logger usage.
            // This will not be logged with a minimum log level of Debug
            this._logger.Verbose("Draw");

            foreach (IDrawSystem drawSystem in this._systemService.GetSystems<IDrawSystem>())
            {
                drawSystem.Draw(gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            // Example logger usage.
            // This will not be logged with a minimum log level of Debug
            this._logger.Verbose("Update");

            this._entitiesSubmissionScheduler.SubmitEntities();
            foreach (IUpdateSystem updateSystem in this._systemService.GetSystems<IUpdateSystem>())
            {
                updateSystem.Update(gameTime);
            }
        }
    }
}