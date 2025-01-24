using Autofac;
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

    public class GameScene : Scene
    {
        private readonly EntitiesSubmissionScheduler _entitiesSubmissionScheduler;
        private readonly EnginesRoot _enginesRoot;
        private readonly GraphicsDevice _graphics;
        private readonly IEntityFactory _entityFactory;

        public GameScene(
            GraphicsDevice graphics,
            EnginesRoot enginesRoot,
            EntitiesSubmissionScheduler entitiesSubmissionScheduler,
            IEntityFactory entityFactory,
            ISystemService systemService,
            ILogger<GameScene> logger) : base(systemService, logger)
        {
            this._entitiesSubmissionScheduler = entitiesSubmissionScheduler;
            this._graphics = graphics;
            this._enginesRoot = enginesRoot;
            this._entityFactory = entityFactory;
        }

        protected override void Initialize(ILifetimeScope scope)
        {
            base.Initialize(scope);

            // Example logger usage.
            this.logger.Debug("Initializing GameScene...");

            foreach (IEngine engine in this.systemService.GetSystems<IEngine>())
            {
                this._enginesRoot.AddEngine(engine);
            }
            // ball
            int num = 0;
            var entityInitializer = this._entityFactory.BuildEntity<BallDescriptor>(0, ExclusiveGroups.BallGroup);
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
                    entityInitializer = this._entityFactory.BuildEntity<BlockDescriptor>((uint)num, ExclusiveGroups.BlockGroup);
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
            entityInitializer = this._entityFactory.BuildEntity<PaddleDescriptor>(3, ExclusiveGroups.PaddleGroup);
            entityInitializer.Init(new Position(0, this._graphics.Viewport.Height - 16));
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // TODO: Put this into a system
            this._entitiesSubmissionScheduler.SubmitEntities();
        }
    }
}