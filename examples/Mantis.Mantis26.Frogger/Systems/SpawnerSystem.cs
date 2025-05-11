using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.Frogger.Components;
using Mantis.Mantis26.Frogger.Descriptors;
using Mantis.Mantis26.Frogger.Extensions;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Systems
{
    public class SpawnerSystem(
        EntitiesDB entitiesDb,
        IEntityFactory entityFactory
    ) : ISceneSystem, IUpdateSystem
    {
        private readonly EntitiesDB _entitiesDB = entitiesDb;
        private readonly IEntityFactory _entityFactory = entityFactory;

        [SequenceGroup<UpdateSequenceGroupEnum>(UpdateSequenceGroupEnum.Update)]
        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var groups = this._entitiesDB.FindGroups<Spawner, Time>();

            foreach (var ((spawners, times, count), group) in this._entitiesDB.QueryEntities<Spawner, Time>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    Spawner spawner = spawners[i];
                    ref Time time = ref times[i];

                    time.Value += delta;
                    if(time.Value < spawner.Interval)
                    {
                        continue;
                    }

                    this._entityFactory.BuildCar(spawner.Position, spawner.Velocity);
                    time.Value -= spawner.Interval;
                }
            }
        }
    }
}
