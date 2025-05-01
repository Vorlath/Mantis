using System.Diagnostics;
using Mantis.Core.MonoGame.Common;
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

        public void Update(GameTime gameTime)
        {
            var groups = this.entitiesDB.FindGroups<Velocity, Transform2D, PlayerState, Gravity>();
            foreach (var ((velocities, positions, playerStates, gravities, count), group) in this.entitiesDB.QueryEntities<Velocity, Transform2D, PlayerState, Gravity>(groups))
            {
                var (animations, _) = this.entitiesDB.QueryEntities<Animated>(group);
                for (int i = 0; i < count; i++)
                {
                    ref Velocity velocity = ref velocities[i];
                    ref Transform2D position = ref positions[i];
                    ref PlayerState playerState = ref playerStates[i];
                    ref Gravity gravity = ref gravities[i];
                    ref Animated animation = ref animations[i];


                    Update(ref playerState, ref velocity, ref position, ref gravity, ref animation, gameTime);
                }
            }
        }

        private static void Update(ref PlayerState playerState, ref Velocity velocity, ref Transform2D position, ref Gravity gravity, ref Animated animation, GameTime gameTime)
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
                animation.Animation.Type = AnimationType.GetAnimationTypeById(0);
                playerState.isGrounded = true;
                position.Position.Y = 924.0f;
                Debug.WriteLine(position.Position.Y);
            }
            else
            {
                if (!playerState.isGrounded)
                {
                    velocity.Value.Y += (gravity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }
            position.Position += (velocity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}