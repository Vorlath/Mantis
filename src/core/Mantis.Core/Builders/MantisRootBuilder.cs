using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Constants;

namespace Mantis.Core.Builders
{
    public class MantisRootBuilder : IMantisRootBuilder
    {
        public ContainerBuilder ContainerBuilder { get; } = new ContainerBuilder();

        public IEnvironmentVariableBuilder EnvironmentVariables { get; }

        public MantisRootBuilder()
        {
            this.ContainerBuilder = new ContainerBuilder();
            this.EnvironmentVariables = new EnvironmentVariableBuilder();

            // Configure
            this.ContainerBuilder.RegisterType<MantisRoot>().AsSelf().As<IMantisRoot>().SingleInstance();
            this.ContainerBuilder.Register<IMantisScope>(x => x.Resolve<IMantisRoot>().MainScope).SingleInstance();

            // Add default values
            this.EnvironmentVariables
                .Add(MantisCoreVariables.Environment.Company.Create("Vorlath"))
                .Add(MantisCoreVariables.Environment.Project.Create("Mantis"));
        }

        public IMantisRoot Build()
        {
            this.ContainerBuilder.RegisterInstance<IEnumerable<IEnvironmentVariable>>(this.EnvironmentVariables.ToArray());

            return this.ContainerBuilder.Build().Resolve<IMantisRoot>();
        }
    }
}