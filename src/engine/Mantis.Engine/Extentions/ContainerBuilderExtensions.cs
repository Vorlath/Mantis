using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis.Engine.Extentions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterMonoGameServices(this ContainerBuilder builder, ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {
            builder.RegisterType<SpriteBatch>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterInstance<ContentManager>(content);
            builder.RegisterInstance<GraphicsDeviceManager>(graphicsDeviceManager);
            builder.RegisterInstance<GraphicsDevice>(graphicsDeviceManager.GraphicsDevice);
        }
    }
}
