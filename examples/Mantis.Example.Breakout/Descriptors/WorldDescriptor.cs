using Mantis.Example.Breakout.Components;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Descriptors
{
    public class WorldDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;

        private static readonly IComponentBuilder[] _componentsToBuild =
        [
            new ComponentBuilder<Boundary>(new Boundary())
        ];
    }
}