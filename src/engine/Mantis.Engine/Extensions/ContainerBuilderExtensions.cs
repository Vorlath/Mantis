using Autofac;
using Mantis.Engine.Common;
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

        public static ContainerBuilder RegisterSystem<TSystem>(this ContainerBuilder builder) where TSystem : ISystem
        {
            builder.RegisterType<TSystem>().As<ISystem>().InstancePerLifetimeScope();
            return builder;
        }
    }
}