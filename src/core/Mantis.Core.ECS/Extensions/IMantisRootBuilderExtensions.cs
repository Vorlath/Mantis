using Autofac;
using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Extensions;
using Mantis.Core.ECS.Common.Services;
using Mantis.Core.ECS.Extensions.Svelto;
using Mantis.Core.ECS.Services;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Mantis.Core.Extensions
{
    public static class IMantisRootBuilderExtensions
    {
        public static IMantisRootBuilder RegisterECSServices(this IMantisRootBuilder builder)
        {
            builder.RegisterType<EntitiesSubmissionScheduler>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EnginesRoot>().AsSelf().InstancePerLifetimeScope();
            builder.Register<EntitiesDB>(ctx => ctx.Resolve<EnginesRoot>().GetEntitiesDB()).InstancePerLifetimeScope();
            builder.Register(EntityFactoryResolver).InstancePerLifetimeScope();
            builder.Register(EntityFunctionsResolver).InstancePerLifetimeScope();
            builder.RegisterType<EntityService>().As<IEntityService>().InstancePerLifetimeScope();

            return builder;
        }

        private static IEntityFactory EntityFactoryResolver(IComponentContext context)
        {
            return context.Resolve<EnginesRoot>().GenerateEntityFactory();
        }

        private static IEntityFunctions EntityFunctionsResolver(IComponentContext context)
        {
            return context.Resolve<EnginesRoot>().GenerateEntityFunctions();
        }
    }
}