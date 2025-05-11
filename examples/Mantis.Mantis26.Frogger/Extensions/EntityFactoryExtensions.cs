using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Mantis26.Frogger.Components;
using Mantis.Mantis26.Frogger.Descriptors;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Extensions
{
    public static class EntityFactoryExtensions
    {
        private static uint _entityId = uint.MinValue;

        public static EntityInitializer BuildFrog(this IEntityFactory entityFactory, Vector2 position)
        {
            EntityInitializer initilaizer = entityFactory.BuildEntity<FrogDescriptor>(_entityId++, ExclusiveGroups.Frog);
            initilaizer.Init<Position>(new Position() { Value = position });
            initilaizer.Init<Controllable>(new Controllable() { Target = position, Origin = position });

            return initilaizer;
        }

        public static EntityInitializer BuildCar(this IEntityFactory entityFactory, Vector2 position, Vector2 velocity)
        {
            EntityInitializer initilaizer = entityFactory.BuildEntity<CarDescriptor>(_entityId++, ExclusiveGroups.Car);
            initilaizer.Init<Position>(new Position() { Value = position });
            initilaizer.Init<Velocity>(new Velocity() { Value = velocity });

            return initilaizer;
        }

        public static EntityInitializer BuildSpawner(this IEntityFactory entityFactory, Spawner spawner)
        {
            EntityInitializer initilaizer = entityFactory.BuildEntity<SpawnerDescriptor>(_entityId++, ExclusiveGroups.Spawner);
            initilaizer.Init<Spawner>(spawner);

            return initilaizer;
        }
    }
}
