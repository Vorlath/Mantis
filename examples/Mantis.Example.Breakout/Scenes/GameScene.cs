using Mantis.Core.Logging.Common;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
using Mantis.Example.Breakout.Components;
using Mantis.Example.Breakout.Descriptors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Mantis.Example.Breakout.Scenes
{
    public static class ExclusiveGroups
    {

        public static readonly ExclusiveGroup BallGroup = new();
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
            ISystemService systemService)
        {
            this._entitiesSubmissionScheduler = entitiesSubmissionScheduler;
            this._logger = logger;
            this._systemService = systemService;
            foreach (IEngine engine in this._systemService.GetSystems<IEngine>())
            {
                enginesRoot.AddEngine(engine);
            }
            // ball
            int num = 0;
            var entityInitializer = entityFactory.BuildEntity<BallDescriptor>(0, ExclusiveGroups.BallGroup);
            entityInitializer.Init(new Position(100, 500));
            entityInitializer.Init(new Velocity(200, 300));
            entityInitializer.Init(new Size(16, 16));
            num++;

            //block
            Color[] colors = [Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet];
            for (int i = 1; i < 8; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    entityInitializer = entityFactory.BuildEntity<BlockDescriptor>((uint)num, ExclusiveGroups.BlockGroup);
                    entityInitializer.Init(new Position((j * 64), (i * 32)));
                    entityInitializer.Init(new Size(64, 32));
                    entityInitializer.Init(new Health(1));
                    entityInitializer.Init(new Collidable());
                    entityInitializer.Init(new Texture(Enums.TextureEnum.Block, colors[i - 1]));
                    num++;
                }
            }
            // entityInitializer = entityFactory.BuildEntity<BlockDescriptor>(3, ExclusiveGroups.blockGroup);
            // entityInitializer.Init(new Position(300, 100));
            // entityInitializer.Init(new Size(64, 32));
            // entityInitializer.Init(new Health(1));
            // entityInitializer.Init(new Collidable());

            // paddle
            entityInitializer = entityFactory.BuildEntity<PaddleDescriptor>(3, ExclusiveGroups.PaddleGroup);
            entityInitializer.Init(new Position(0, graphics.Viewport.Height - 16));

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