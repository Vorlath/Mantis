using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.OnlyUp.Components;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Engines
{
    public class MovementEngine() : IQueryingEntitiesEngine, IUpdateSystem, ISceneSystem
    {

        public EntitiesDB entitiesDB { get; set; } = null!;

        public void Ready()
        {
            //    throw new NotImplementedException();
        }

        [SequenceGroup<UpdateSequenceGroupEnum>(UpdateSequenceGroupEnum.Update)]
        public void Update(GameTime gameTime)
        {
            var groups = this.entitiesDB.FindGroups<Velocity, Transform2D, PlayerState, Gravity>();
            foreach (var ((velocities, positions, playerStates, gravities, count), group) in this.entitiesDB.QueryEntities<Velocity, Transform2D, PlayerState, Gravity>(groups))
            {
                var (animations, collisions, _) = this.entitiesDB.QueryEntities<Animated, Collidable>(group);
                for (int i = 0; i < count; i++)
                {
                    ref Velocity velocity = ref velocities[i];
                    ref Transform2D position = ref positions[i];
                    ref PlayerState playerState = ref playerStates[i];
                    ref Gravity gravity = ref gravities[i];
                    ref Animated animation = ref animations[i];
                    ref Collidable collision = ref collisions[i];


                    Update(ref playerState, ref velocity, ref position, ref gravity, ref animation, ref collision, gameTime);
                }
            }
        }

        private static void Update(ref PlayerState playerState, ref Velocity velocity, ref Transform2D position, ref Gravity gravity, ref Animated animation, ref Collidable collision, GameTime gameTime)
        {
            // set velocity to 0

            if (playerState.isGrounded)
            {
                velocity.Value.Y = 0;
                velocity.Value.X = 0;
            }


            //This is temporary
            if (position.Position.Y > 925 && !playerState.isGrounded)
            {
                playerState.isGrounded = true;
                position.Position.Y = 924.0f;
            }
            else
            {
                if (!playerState.isGrounded)
                {
                    velocity.Value.Y += (gravity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }
            position.Position += (velocity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // new position based on the entity's updated position, while keeping the box's size
            //collision.CollisionBox = RectangleHelper.CreateCollisionBoundsF(position.Position, collision.Offset, collision.CollisionBox.Size);
            collision.CollisionBox = RectangleHelper.CreateBoundsF(position.Position, collision.CollisionBox.Size);
        }
    }
}