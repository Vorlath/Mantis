using Autofac;

namespace Mantis.Engine.Common.Systems
{
    public interface IInitializeSystem : ISystem
    {
        void Initialize(ILifetimeScope scope);
    }
}
