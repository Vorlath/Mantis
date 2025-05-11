using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.Frogger.Components;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Systems
{
    public class CollisionSystem(EntitiesDB entitiesDb) : ISceneSystem, IUpdateSystem
    {
        private readonly EntitiesDB _entitiesDb = entitiesDb;

        [SequenceGroup<UpdateSequenceGroupEnum>(UpdateSequenceGroupEnum.PostUpdate)]
        public void Update(GameTime gameTime)
        {
            var (positions, controllables, sizes, count) = this._entitiesDb.QueryEntities<Position, Controllable, Size>(ExclusiveGroups.Frog);
            for(int i=0; i<count; i++)
            {
                ref Position position = ref positions[i];
                ref Controllable controllable = ref controllables[i];
                Size size = sizes[i];

                this.CheckFrog(ref position, ref controllable, new RectangleF(position.Value.X, position.Value.Y, size.Value.X, size.Value.Y));
            }
        }

        private void CheckFrog(ref Position position, ref Controllable controllable, RectangleF bounds)
        {
            var groups = this._entitiesDb.FindGroups<Collidable, Position, Size>();

            foreach (var ((positions, sizes, count), _) in this._entitiesDb.QueryEntities<Position, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    RectangleF obstacle = new RectangleF(positions[i].Value.X, positions[i].Value.Y, sizes[i].Value.X, sizes[i].Value.Y);

                    if(obstacle.IntersectsWith(bounds) == false)
                    {
                        continue;
                    }

                    Vector2 respawn = new Vector2(0, 6);
                    position.Value = respawn;
                    controllable.Target = respawn;
                    controllable.Origin = respawn;
                    controllable.Delta = 0;

                    return;
                }
            }
        }
    }
}
