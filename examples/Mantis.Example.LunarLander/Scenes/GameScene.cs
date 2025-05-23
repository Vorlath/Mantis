﻿using Mantis.Core.Common;
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

    public class GameScene : BaseScene
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
            ContentManager content) : base(systemService)
        {
            this._entitiesSubmissionScheduler = entitiesSubmissionScheduler;
            this._logger = logger;
            this._systemService = systemService;
            foreach (IEngine engine in this._systemService.GetSystems<IEngine>())
            {
                enginesRoot.AddEngine(engine);
            }

            var _texture = content.Load<Texture2D>("LanderSheet");
            SpriteSheet SpriteSheet = new SpriteSheet(_texture, [
                new SpriteData("1", new Rectangle(0, 0, 16, 16)),
                new SpriteData("2", new Rectangle(16, 0, 16, 16)),
                new SpriteData("3", new Rectangle(32, 0, 16, 16)),
                new SpriteData("4", new Rectangle(0, 16, 16, 16)),
                new SpriteData("5", new Rectangle(16, 16, 16, 16))
                ]);

            // Name this
            AnimationType LanderIdle = SpriteSheet.CreateAnimationType(AnimationTypes.LanderIdle, [
                new AnimationFrameContext("1", 10000)
            ]);
            _ = SpriteSheet.CreateAnimationType(AnimationTypes.ThrustOn, [
                new AnimationFrameContext("2", 100),
                new AnimationFrameContext("3", 500)
            ]);
            _ = SpriteSheet.CreateAnimationType(AnimationTypes.ThrustOff, [
                new AnimationFrameContext("3", 500),
                new AnimationFrameContext("2", 500),
                new AnimationFrameContext("1", 500)
            ]);
            _ = SpriteSheet.CreateAnimationType(AnimationTypes.RotateClockwise, [
                new AnimationFrameContext("4", 500)
            ]);
            _ = SpriteSheet.CreateAnimationType(AnimationTypes.RotateCounterclockwise, [
                new AnimationFrameContext("5", 500)
            ]);

            // lander

            var entityInitializer = entityFactory.BuildEntity<LanderDescriptor>(0, ExclusiveGroups.LanderGroup);
            entityInitializer.Init(new Transform2D(0, 0, 270));
            entityInitializer.Init(new Velocity(75, 0));
            entityInitializer.Init(new Gravity(5));
            entityInitializer.Init(new Size(32, 32));
            entityInitializer.Init(new Animated(LanderIdle));

            // Example logger usage.
            this._logger.Debug("Created GameScene!");
        }

        public override void Draw(GameTime gameTime)
        {
            // Example logger usage.
            // This will not be logged with a minimum log level of Debug
            this._logger.Verbose("Draw");

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            // Example logger usage.
            // This will not be logged with a minimum log level of Debug
            this._logger.Verbose("Update");

            this._entitiesSubmissionScheduler.SubmitEntities();

            base.Update(gameTime);
        }
    }
}