using Autofac;

namespace Mantis.Core.Common.Builders
{
    public interface IMantisBuilder
    {
        /// <summary>
        /// The raw autofac container builder
        /// </summary>
        ContainerBuilder ContainerBuilder { get; }
    }
}