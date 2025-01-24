using Autofac;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;
using Mantis.Engine.Extensions;
using Microsoft.Xna.Framework;

namespace Mantis.Engine
{
    public class MantisEngine : IMantisEngine
    {
        private readonly IContainer _container;
        private readonly ISystemService<IGlobalSystem> _globalSystems;
        public ISceneService Scenes { get; }
        public MantisEngine(Action<ContainerBuilder> build)
        {
            // Create new ContainerBuilder and register required MantisEngine services
            ContainerBuilder builder = new();
            builder.RegisterEngineServices();
            build(builder);

            this._container = builder.Build();

            this._globalSystems = this._container.Resolve<ISystemService<IGlobalSystem>>();
            this.Scenes = this._container.Resolve<ISceneService>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IUpdateSystem globalSystem in this._globalSystems.GetSystems<IUpdateSystem>())
            {
                globalSystem.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IDrawSystem globalSystem in this._globalSystems.GetSystems<IDrawSystem>())
            {
                globalSystem.Draw(gameTime);
            }
        }
    }
}