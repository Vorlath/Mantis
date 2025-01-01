using Autofac;
using Mantis.Core.Extensions;
using Mantis.Engine;
using Mantis.Engine.Extensions;
using Mantis.Example.Breakout.Engines;
using Mantis.Example.Breakout.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.Breakout
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
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
            this.IsFixedTimeStep = false;

            _graphics.PreparingDeviceSettings += (s, e) =>
            {
                _graphics.PreferMultiSampling = true;
                e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 8;
                e.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
                e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
            };
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.ApplyChanges();
            _mantis = new MantisEngine(builder =>
            {
                builder.RegisterMonoGameServices(this.Content, _graphics).RegisterECSServices();
                builder.RegisterType<GameScene>().AsSelf().InstancePerLifetimeScope();
                builder.RegisterType<TextureEngine>().As<IEngine>().InstancePerLifetimeScope();
                builder.RegisterType<MovementEngine>().As<IEngine>().InstancePerLifetimeScope();
                builder.RegisterType<CollisionEngine>().As<IEngine>().InstancePerLifetimeScope();
                builder.RegisterType<ControllableEngine>().As<IEngine>().InstancePerLifetimeScope();
            });
            _mantis.Scenes.Create<GameScene>();
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

            _mantis.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.DarkGray);

            _mantis.Draw(gameTime);
        }
    }
}
