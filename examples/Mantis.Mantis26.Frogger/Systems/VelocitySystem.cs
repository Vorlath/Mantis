using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.Frogger.Components;
using Mantis.Mantis26.Frogger.Descriptors;
using Mantis.Mantis26.Frogger.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Systems
{
    internal class VelocitySystem(
        EntitiesDB entitiesDb,
        Camera camera,
        IEntityFunctions functions
    ) : ISceneSystem, IUpdateSystem
    {
        private readonly EntitiesDB _entitiesDB = entitiesDb;
        private readonly Camera _camera = camera;
        private readonly IEntityFunctions _functions = functions;

        [SequenceGroup<UpdateSequenceGroupEnum>(UpdateSequenceGroupEnum.Update)]
        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var groups = this._entitiesDB.FindGroups<Velocity, Position, Size>();

            foreach (var ((velocities, positions, sizes, egids, count), group) in this._entitiesDB.QueryEntities<Velocity, Position, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Velocity velocity = ref velocities[i];
                    ref Position position = ref positions[i];
                    ref Size size = ref sizes[i];

                    position.Value += velocity.Value * delta;

                    if(position.Value.X > this._camera.ScreenBounds.Right && velocity.Value.X > 0)
                    {
                        this._functions.RemoveEntity<CarDescriptor>(egids[i], group);
                    }

                    if (position.Value.X < this._camera.ScreenBounds.Left - size.Value.X && velocity.Value.X < 0)
                    {
                        this._functions.RemoveEntity<CarDescriptor>(egids[i], group);
                    }
                }
            }
        }
    }
}
