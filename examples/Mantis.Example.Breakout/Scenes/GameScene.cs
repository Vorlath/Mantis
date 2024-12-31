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
            var entityInitializer = entityFactory.BuildEntity<BallDescriptor>(0, ExclusiveGroups.ballGroup);
            entityInitializer.Init(new Position(100, 100));
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
