using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Common.Builders;

namespace Mantis.Core.Builders
{
    public class MantisRootBuilder : IMantisRootBuilder
    {
        public ContainerBuilder ContainerBuilder { get; } = new ContainerBuilder();

        public MantisRootBuilder()
        {
            this.ContainerBuilder = new ContainerBuilder();

            // Configure
            this.ContainerBuilder.RegisterType<MantisRoot>().As<IMantisRoot>().SingleInstance();
            this.ContainerBuilder.Register<IMantisScope>(x => x.Resolve<IMantisRoot>().MainScope).SingleInstance();
        }

        public IMantisRoot Build()
        {
            return this.ContainerBuilder.Build().Resolve<IMantisRoot>();
        }
    }
}
