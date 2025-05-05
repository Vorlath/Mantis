using Autofac;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Extensions;
using Mantis.Core.Logging.Common.Enums;
using Mantis.Core.Logging.Common.Extensions;
using Mantis.Core.Logging.Serilog.Extensions;
using Mantis.Engine;
using Mantis.Engine.Common.Extensions;
using Mantis.Engine.Extensions;
using Mantis.Mantis26.OnlyUp.Engines;
using Mantis.Mantis26.OnlyUp.Scenes;
using Mantis.Mantis26.OnlyUp.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Mantis26.OnlyUp
{
    public sealed class Game1 : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly MantisEngine _mantis;


        // https://community.monogame.net/t/start-in-maximized-window/12264
        // [DllImport("SDL2.dll", CallingConvention = CallingConvention.Cdecl)]
        // public static extern void SDL_MaximizeWindow(IntPtr window);


        public Game1()
        {
            this._graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
            this.IsFixedTimeStep = false;

            this._graphics.PreparingDeviceSettings += (s, e) =>
            {
                this._graphics.PreferMultiSampling = true;
                e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 8;
                e.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
                e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
                e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = 1000;
                e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = 1920;
            };
            this._graphics.SynchronizeWithVerticalRetrace = false;
            this._graphics.GraphicsProfile = GraphicsProfile.HiDef;
            this._graphics.ApplyChanges();
            this._mantis = new MantisEngine(builder =>
            {
                builder.RegisterCoreServices()
                    .RegisterMonoGameServices(this.Content, this._graphics)
                    .RegisterECSServices();

                // Register serilog logging and the desired logger sinks.
                // Once registered, the logger can be utilized by injecting
                // ILogger<TContext> into your class TContext. See GameScene for an example
                builder.RegisterSerilogLoggingServices(LogLevelEnum.Debug) // Register serilog services with a Debug log level
                    .ConfigureConsoleLoggerSink() // Setup console sink. The log output will be written to the console
                    .ConfigureFileLoggerSink($"logs/log_{DateTime.Now:yyyy-dd-MM}.txt"); // Setup file sink. The created file will exist in the exe's build path at ./logs/log_yyyy-dd-MM.txt

                builder.RegisterType<GameScene>().AsSelf().InstancePerLifetimeScope();
                builder.RegisterSceneSystem<AnimationSystem>();
                builder.RegisterSceneSystem<TextureEngine>();
                builder.RegisterSceneSystem<MovementEngine>();
                builder.RegisterSceneSystem<CollisionEngine>();
                builder.RegisterSceneSystem<ControllableEngine>();

            });
            this._mantis.Scenes.Create<GameScene>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            Environment.Exit(0);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            base.Update(gameTime);

            this._mantis.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.GraphicsDevice.Clear(Color.Black);

            this._mantis.Draw(gameTime);
        }
    }
}