using Autofac;
using Mantis.Core.ECS.Common.Services;
using Mantis.Core.ECS.Services;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Mantis.Core.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterECSServices(this ContainerBuilder builder)
        {
            builder.RegisterType<EntitiesSubmissionScheduler>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EnginesRoot>().AsSelf().InstancePerLifetimeScope();
            builder.Register(EntityFactoryResolver).InstancePerLifetimeScope();
            builder.RegisterType<EntityService>().As<IEntityService>().InstancePerLifetimeScope();

            return builder;
        }

        private static IEntityFactory EntityFactoryResolver(IComponentContext context)
        {
            return context.Resolve<EnginesRoot>().GenerateEntityFactory();
        }
    }
}
