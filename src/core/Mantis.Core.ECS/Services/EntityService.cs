using Mantis.Core.ECS.Common.Services;
using Svelto.ECS;

namespace Mantis.Core.ECS.Services
{
    public class EntityService(IEntityFactory entityFactory) : IEntityService
    {
    }
}