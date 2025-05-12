using System.Text.Json;
using Mantis.Core.Logging.Common;
using Mantis.Core.MonoGame.Common;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
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

    public class JsonRectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class GameScene : BaseScene
    {
        private readonly EntitiesSubmissionScheduler _entitiesSubmissionScheduler;
        private readonly ILogger<GameScene> _logger;

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

            foreach (IEngine engine in this.SystemService.GetSystems<IEngine>())
            {
                enginesRoot.AddEngine(engine);
            }

            /////////////////////////////////////////////////////
            /// MNKY Animations
            var _MNKYTexture = content.Load<Texture2D>("MNKY");
            var MNKYSpriteSheet = new SpriteSheet(_MNKYTexture, [
                new SpriteData("1", new Rectangle(0, 0, 32, 32)),
                new SpriteData("2", new Rectangle(32, 0, 32, 32)),
                new SpriteData("3", new Rectangle(0, 32, 32, 32)),
                new SpriteData("4", new Rectangle(32, 32, 32, 32)),
                new SpriteData("5", new Rectangle(0, 64, 32, 32)),
                new SpriteData("6", new Rectangle(32, 64, 32, 32))
                ]);

            AnimationType MNKYIdleRight = MNKYSpriteSheet.CreateAnimationType([
                new AnimationFrameContext("1", 1000)
            ]);
            MNKYSpriteSheet.CreateAnimationType([
                new AnimationFrameContext("2", 1000)
            ]);
            MNKYSpriteSheet.CreateAnimationType([
                new AnimationFrameContext("3", 1000)
            ]);
            MNKYSpriteSheet.CreateAnimationType([
                new AnimationFrameContext("4", 1000)
            ]);
            MNKYSpriteSheet.CreateAnimationType([
                new AnimationFrameContext("5", 1000)
            ]);
            MNKYSpriteSheet.CreateAnimationType([
                new AnimationFrameContext("6", 1000)
            ]);

            /////////////////////////////////////////////////////
            // MNKY
            var MNKY = entityFactory.BuildEntity<MNKYDescriptor>(0, ExclusiveGroups.LanderGroup);
            MNKY.Init(new Transform2D(8, 0, 0));
            MNKY.Init(new Velocity(75, 0));
            MNKY.Init(new Gravity(1000));
            MNKY.Init(new Size(64, 64));
            MNKY.Init(new Animated(MNKYIdleRight));
            MNKY.Init(new Collidable(new RectangleF(0, 0, 32, 64), new Vector2(16, 0)));
            MNKY.Init(new Controllable());
            MNKY.Init(new PlayerState());
            MNKY.Init(new Jump(150));

            /////////////////////////////////////////////////////
            /// Block
            /// 

            string json = System.IO.File.ReadAllText("Content/Level.json");

            List<JsonRectangle> jsonRectangles = JsonSerializer.Deserialize<List<JsonRectangle>>(json);

            int num = 0;
            foreach (JsonRectangle jsonRectangle in jsonRectangles)
            {
                var Block = entityFactory.BuildEntity<BlockDescriptor>((uint)num, ExclusiveGroups.BlockGroup);
                Block.Init(new Transform2D(jsonRectangle.X * 32, jsonRectangle.Y * 32, 0));
                Block.Init(new Size(32, 32));
                Block.Init(new Collidable(new RectangleF(jsonRectangle.X * 32, jsonRectangle.Y * 32, 32, 32), Vector2.Zero));
                Block.Init(new Texture(Enums.TextureEnum.Block, Color.White));
                num++;
            }

            //int num = 0;
            //var Block = entityFactory.BuildEntity<BlockDescriptor>((uint)num, ExclusiveGroups.BlockGroup);
            //Block.Init(new Transform2D(500, 800, 0));
            //Block.Init(new Size(32, 32));
            //Block.Init(new Collidable(new RectangleF(500, 800, 32, 32), Vector2.Zero));
            //Block.Init(new Texture(Enums.TextureEnum.Block, Color.White));
            //num++;
            //for (int i = 2; i < 10; i++)
            //{
            //    Block = entityFactory.BuildEntity<BlockDescriptor>((uint)num, ExclusiveGroups.BlockGroup);
            //    Block.Init(new Transform2D(32 * i + 500, 800, 0));
            //    Block.Init(new Size(32, 32));
            //    Block.Init(new Collidable(new RectangleF(32 * i + 500, 800, 32, 32), Vector2.Zero));
            //    Block.Init(new Texture(Enums.TextureEnum.Block, Color.White));
            //    num++;
            //}



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