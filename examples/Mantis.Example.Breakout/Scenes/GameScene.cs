using Mantis.Engine.Common;
using Mantis.Example.Breakout.Components;
using Mantis.Example.Breakout.Descriptors;
using Mantis.Example.Breakout.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Mantis.Example.Breakout.Scenes
{
    public static class ExclusiveGroups
    {

        public static ExclusiveGroup ballGroup = new();
        public static ExclusiveGroup blockGroup = new();
        public static ExclusiveGroup wallGroup = new();
        public static ExclusiveGroup paddleGroup = new();
    }

    public class GameScene : IScene
    {
        private readonly EntitiesSubmissionScheduler entitiesSubmissionScheduler;
        private readonly IFrameEngine[] _engines;
        public GameScene(
            EnginesRoot enginesRoot,
            IEnumerable<IEngine> engines,
            IEntityFactory entityFactory,
            EntitiesSubmissionScheduler entitiesSubmissionScheduler,
            GraphicsDevice graphics)
        {
            this.entitiesSubmissionScheduler = entitiesSubmissionScheduler;
            _engines = engines.OfType<IFrameEngine>().ToArray();
            foreach (IEngine engine in engines)
            {
                enginesRoot.AddEngine(engine);
            }
            // ball
            var entityInitializer = entityFactory.BuildEntity<BallDescriptor>(0, ExclusiveGroups.ballGroup);
            entityInitializer.Init(new Position(100, 100));
            entityInitializer.Init(new Velocity(100, 200));
            entityInitializer.Init(new Size(16, 16));

            //block
            // entityInitializer = entityFactory.BuildEntity<BlockDescriptor>(3, ExclusiveGroups.blockGroup);
            // entityInitializer.Init(new Position(300, 100));
            // entityInitializer.Init(new Size(64, 32));
            // entityInitializer.Init(new Health(1));
            // entityInitializer.Init(new Collidable());

            // paddle
            entityInitializer = entityFactory.BuildEntity<PaddleDescriptor>(3, ExclusiveGroups.paddleGroup);
            entityInitializer.Init(new Position(0, graphics.Viewport.Height - 16));
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IFrameEngine engine in _engines)
            {
                engine.Draw(gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            entitiesSubmissionScheduler.SubmitEntities();
            foreach (IFrameEngine engine in _engines)
            {
                engine.Update(gameTime);
            }
        }
    }
}
