using Autofac;
using Mantis.Core.Builders;
using Mantis.Core.Common;
using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Extensions;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Extensions;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;
using Mantis.Engine.Services;
using Mantis.Engine.Systems;
using Microsoft.Xna.Framework;

namespace Mantis.Engine
{
    public class MantisEngine : IMantisEngine
    {
        private readonly IMantisRoot _root;
        private readonly ISystemService<IGlobalSystem> _globalSystems;
        public ISceneService Scenes { get; }
        public MantisEngine(Action<IMantisRootBuilder> build)
        {
            MantisRootBuilder builder = new MantisRootBuilder();
            builder.RegisterType<SceneService>().As<ISceneService>().SingleInstance();
            builder.RegisterType<SystemService<IGlobalSystem>>().As<ISystemService<IGlobalSystem>>().SingleInstance();
            builder.RegisterType<SystemService>().As<ISystemService>().As<ISystemService<ISceneSystem>>().InstancePerLifetimeScope();
            builder.RegisterGlobalSystem<SceneFrameSystem>();

            build(builder);
            this._root = builder.Build();

            this._globalSystems = this._root.Resolve<ISystemService<IGlobalSystem>>();
            this.Scenes = this._root.Resolve<ISceneService>();
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