using Mantis.Engine.Common;
using Mantis.Example.Breakout.Components;
using Mantis.Example.Breakout.Descriptors;
using Mantis.Example.Breakout.Engines;
using Microsoft.Xna.Framework;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Mantis.Example.Breakout.Scenes
{
    public static class ExclusiveGroups
    {

        public static ExclusiveGroup ballGroup = new ExclusiveGroup();
        public static ExclusiveGroup blockGroup = new ExclusiveGroup();
        public static ExclusiveGroup wallGroup = new ExclusiveGroup();
    }

    public class GameScene : IScene
    {
        EntitiesSubmissionScheduler entitiesSubmissionScheduler;
        private IFrameEngine[] _engines;
        public GameScene(EnginesRoot enginesRoot, IEnumerable<IEngine> engines, IEntityFactory entityFactory, EntitiesSubmissionScheduler entitiesSubmissionScheduler)
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
            entityInitializer.Init(new Size(32, 32));

            entityInitializer = entityFactory.BuildEntity<BallDescriptor>(1, ExclusiveGroups.ballGroup);
            entityInitializer.Init(new Position(200, 200));
            entityInitializer.Init(new Velocity(100, 200));
            entityInitializer.Init(new Size(32, 32));

            entityInitializer = entityFactory.BuildEntity<BallDescriptor>(2, ExclusiveGroups.ballGroup);
            entityInitializer.Init(new Position(300, 200));
            entityInitializer.Init(new Velocity(100, 200));
            entityInitializer.Init(new Size(32, 32));

            //block
            entityInitializer = entityFactory.BuildEntity<BlockDescriptor>(3, ExclusiveGroups.blockGroup);
            entityInitializer.Init(new Position(300, 100));
            entityInitializer.Init(new Size(64, 32));
            entityInitializer.Init(new Health(1));
            entityInitializer.Init(new Collidable());

            entityInitializer = entityFactory.BuildEntity<BlockDescriptor>(4, ExclusiveGroups.blockGroup);
            entityInitializer.Init(new Position(500, 100));
            entityInitializer.Init(new Size(64, 32));
            entityInitializer.Init(new Health(1));
            entityInitializer.Init(new Collidable());

            entityInitializer = entityFactory.BuildEntity<BlockDescriptor>(5, ExclusiveGroups.blockGroup);
            entityInitializer.Init(new Position(300, 300));
            entityInitializer.Init(new Size(64, 32));
            entityInitializer.Init(new Health(1));
            entityInitializer.Init(new Collidable());

            entityInitializer = entityFactory.BuildEntity<BlockDescriptor>(6, ExclusiveGroups.blockGroup);
            entityInitializer.Init(new Position(500, 300));
            entityInitializer.Init(new Size(64, 32));
            entityInitializer.Init(new Health(1));
            entityInitializer.Init(new Collidable());

            entityInitializer = entityFactory.BuildEntity<BallDescriptor>(7, ExclusiveGroups.ballGroup);
            entityInitializer.Init(new Position(400, 200));
            entityInitializer.Init(new Velocity(100, 200));
            entityInitializer.Init(new Size(32, 32));
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
