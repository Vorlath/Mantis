namespace Mantis.Engine.Common.Systems
{
    /// <summary>
    /// Represents an <see cref="ISystem"/> that has a global scope.
    /// Generally a singleton registered via <see cref="Extensions.ContainerBuilderExtensions.RegisterGlobalSystem{TSystem}(Autofac.ContainerBuilder)"/>
    /// and managed via <see cref="IMantisEngine"/> implementation
    /// </summary>
    public interface IGlobalSystem : ISystem
    {
    }
}