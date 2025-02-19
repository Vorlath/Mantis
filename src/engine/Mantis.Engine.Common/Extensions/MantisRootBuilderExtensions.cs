using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Extensions;
using Mantis.Engine.Common.Systems;

namespace Mantis.Engine.Common.Extensions
{
    public static class MantisRootBuilderExtensions
    {
        public static IMantisRootBuilder RegisterSceneSystem<TSystem>(this IMantisRootBuilder builder)
            where TSystem : ISceneSystem
        {
            builder.RegisterType<TSystem>().As<ISceneSystem>().InstancePerLifetimeScope();
            return builder;
        }

        public static IMantisRootBuilder RegisterGlobalSystem<TSystem>(this IMantisRootBuilder builder)
            where TSystem : IGlobalSystem
        {
            builder.RegisterType<TSystem>().As<IGlobalSystem>().SingleInstance();
            return builder;
        }
    }
}