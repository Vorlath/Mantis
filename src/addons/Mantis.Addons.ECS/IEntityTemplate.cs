using Mantis.Core.Common;
using Svelto.ECS;

namespace Mantis.Addons.ECS
{
    public interface IEntityTemplate
    {
        Key<IEntityTemplate> Key { get; }

        IReadOnlyCollection<IComponentBuilder> ComponentBuilders { get; }
    }
}
