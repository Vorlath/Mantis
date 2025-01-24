using Autofac;
using Mantis.Engine.Common.Extensions;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;
using Mantis.Engine.Services;
using Mantis.Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Engine.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterMonoGameServices(this ContainerBuilder builder, ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {
            builder.RegisterType<SpriteBatch>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterInstance<ContentManager>(content);
            builder.RegisterInstance<GraphicsDeviceManager>(graphicsDeviceManager);
            builder.RegisterInstance<GraphicsDevice>(graphicsDeviceManager.GraphicsDevice);
            return builder;
        }

        public static ContainerBuilder RegisterEngineServices(this ContainerBuilder builder)
        {
            builder.RegisterType<SceneService>().As<ISceneService>().SingleInstance();
            builder.RegisterType<SystemService<IGlobalSystem>>().As<ISystemService<IGlobalSystem>>().SingleInstance();
            builder.RegisterType<SystemService>().As<ISystemService>().As<ISystemService<ISceneSystem>>().InstancePerLifetimeScope();
            builder.RegisterGlobalSystem<SceneFrameSystem>();

            return builder;
        }
    }
}