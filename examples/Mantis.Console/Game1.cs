using Autofac;
using Mantis.Console.Scenes;
using Mantis.Console.Services;
using Mantis.Core.Extensions;
using Mantis.Engine;
using Mantis.Engine.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Console
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
            };
            this._graphics.SynchronizeWithVerticalRetrace = false;
            this._graphics.GraphicsProfile = GraphicsProfile.HiDef;
            this._graphics.ApplyChanges();
            this._mantis = new MantisEngine(builder =>
            {
                builder.RegisterMonoGameServices(this.Content, this._graphics).RegisterECSServices();
                builder.RegisterType<BrickService>().AsSelf().InstancePerLifetimeScope();
                builder.RegisterType<BreakoutScene>().AsSelf().InstancePerLifetimeScope();
            });
            this._mantis.Scenes.Create<BreakoutScene>();
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

            this.GraphicsDevice.Clear(Color.DarkGray);

            this._mantis.Draw(gameTime);
        }
    }
}