using Mantis.Mantis26.OnlyUp.Components;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Descriptors
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