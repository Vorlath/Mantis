namespace Mantis.Engine.Common.Systems
{
    /// <summary>
    /// Represents an <see cref="ISystem"/> that has a scene scope.
    /// Generally a scoped service registered via <see cref="Extensions.MantisRootBuilderExtensions.RegisterSceneSystem{TSystem}(Autofac.ContainerBuilder){TSystem}(Autofac.ContainerBuilder)"/>
    /// and managed via <see cref="IScene"/> implementation
    /// </summary>
    public interface ISceneSystem : ISystem
    {
    }
}