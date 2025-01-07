using Autofac;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Services;
using Microsoft.Xna.Framework;

namespace Mantis.Engine
{
    public class MantisEngine
    {
        private readonly IContainer _container;

        public ISceneService Scenes { get; }
        public MantisEngine(Action<ContainerBuilder> customBuilder)
        {
            ContainerBuilder builder = new();
            builder.RegisterType<SceneService>().As<ISceneService>().SingleInstance();
            customBuilder(builder);
            this._container = builder.Build();

            this.Scenes = this._container.Resolve<ISceneService>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IScene scene in this.Scenes.GetAll())
            {
                scene.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IScene scene in this.Scenes.GetAll())
            {
                scene.Draw(gameTime);
            }
        }
    }
}