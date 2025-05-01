using Mantis.Core.Logging.Common;
using Mantis.Core.MonoGame.Common;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.OnlyUp.Components;
using Mantis.Mantis26.OnlyUp.Descriptors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Mantis.Mantis26.OnlyUp.Scenes
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

            var _texture = content.Load<Texture2D>("MNKY");
            var SpriteSheet = new SpriteSheet(_texture, [
                new SpriteData("1", new Rectangle(0, 0, 32, 32)),
                new SpriteData("2", new Rectangle(32, 0, 32, 32)),
                new SpriteData("3", new Rectangle(0, 32, 32, 32)),
                new SpriteData("4", new Rectangle(32, 32, 32, 32)),
                new SpriteData("5", new Rectangle(0, 64, 32, 32)),
                new SpriteData("6", new Rectangle(32, 64, 32, 32))
                ]);

            // Name this
            AnimationType MNKYIdleRight = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("1", 10000)
            ]);
            AnimationType MNKYIdleLeft = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("2", 10000)

            ]);
            _ = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("3", 500)
            ]);
            _ = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("4", 500)
            ]);
            _ = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("5", 500)
            ]);
            _ = SpriteSheet.CreateAnimationType([
                new AnimationFrameContext("6", 500)
            ]);

            // lander

            var MNKY = entityFactory.BuildEntity<MNKYDescriptor>(0, ExclusiveGroups.LanderGroup);
            MNKY.Init(new Transform2D(0, 0, 0));
            MNKY.Init(new Velocity(75, 0));
            MNKY.Init(new Gravity(1000));
            MNKY.Init(new Size(64, 64));
            MNKY.Init(new Animated(MNKYIdleRight));
            MNKY.Init(new Controllable());
            MNKY.Init(new PlayerState());
            MNKY.Init(new Jump(100));

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