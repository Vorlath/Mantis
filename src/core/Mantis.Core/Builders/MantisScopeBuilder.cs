using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Common.Builders;

namespace Mantis.Core.Builders
{
    public class MantisScopeBuilder : IMantisScopeBuilder
    {
        public ContainerBuilder ContainerBuilder { get; }
        public IMantisRoot Root { get; }

        public MantisScopeBuilder(ContainerBuilder containerBuilder, IMantisRoot root)
        {
            this.ContainerBuilder = containerBuilder;
            this.Root = root;

            // Configure
            this.ContainerBuilder.RegisterType<MantisScope>().As<IMantisScope>().InstancePerLifetimeScope();
        }
    }
}