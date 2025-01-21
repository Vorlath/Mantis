using Autofac;
using Mantis.Engine.Common.Systems;

namespace Mantis.Engine.Common.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterSceneSystem<TSystem>(this ContainerBuilder builder)
            where TSystem : ISceneSystem
        {
            builder.RegisterType<TSystem>().As<ISceneSystem>().InstancePerLifetimeScope();
            return builder;
        }

        public static ContainerBuilder RegisterGlobalSystem<TSystem>(this ContainerBuilder builder)
            where TSystem : IGlobalSystem
        {
            builder.RegisterType<TSystem>().As<IGlobalSystem>().SingleInstance();
            return builder;
        }
    }
}