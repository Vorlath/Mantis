using Mantis.Core.Common.Attributes;
using Mantis.Core.MonoGame.Common;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.OnlyUp.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Engines
{
    internal class ControllableEngine : IEngine, IQueryingEntitiesEngine, IUpdateSystem, ISceneSystem
    {
        public EntitiesDB entitiesDB { get; set; } = null!;

        public void Ready()
        {
            // throw new NotImplementedException();
        }

        [SequenceGroup<UpdateSequenceGroupEnum>(UpdateSequenceGroupEnum.Update)]
        public void Update(GameTime gameTime)
        {


            // float targetVelocityModifier = GetTargetVelocityModifier();

            var groups = this.entitiesDB.FindGroups<Animated, Controllable, Velocity, Transform2D>();
            foreach (var ((animations, controllables, velocities, transform2Ds, _, count), group) in this.entitiesDB.QueryEntities<Animated, Controllable, Velocity, Transform2D>(groups))
            {
                var (playerStates, jumps, directions, _) = this.entitiesDB.QueryEntities<PlayerState, Jump, Direction>(group);
                for (int i = 0; i < count; i++)
                {
                    ref Controllable controllable = ref controllables[i];
                    ref Velocity velocity = ref velocities[i];
                    ref Transform2D transform2D = ref transform2Ds[i];
                    ref Animated animated = ref animations[i];
                    ref PlayerState playerState = ref playerStates[i];
                    ref Direction direction = ref directions[i];
                    ref Jump jump = ref jumps[i];

                    GetJump(ref jump, ref playerState, ref velocity, ref animated, ref direction, gameTime);
                    GetDirection(ref playerState, ref animated, ref direction);
                    getAnimationState(ref playerState, ref direction, ref animated);
                    //if (GetThrusters() == 1)
                    //{
                    //    if (animated.Animation.TypeId != 1)
                    //    {
                    //        animated.Animation.Type = AnimationType.GetAnimationTypeById(1);
                    //    }
                    //}
                    //else
                    //{
                    //    if (animated.Animation.TypeId != 0)
                    //    {
                    //        animated.Animation.Type = AnimationType.GetAnimationTypeById(0);
                    //    }
                    //}

                    // transform2D.Rotation += 100f * targetVelocityModifier * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //velocity.Value += new Vector2(MathF.Sin(transform2D.Rotation * (MathF.PI / 180)), -MathF.Cos(transform2D.Rotation * (MathF.PI / 180))) * (GetThrusters() * 0.0012f);
                    //velocity.Value.X = MathHelper.Lerp(velocity.Value.X, controllable.TargetVelocity * targetVelocityModifier, (float)gameTime.ElapsedGameTime.TotalSeconds * 2f);
                }
            }
        }

        private static void getAnimationState(ref PlayerState playerState, ref Direction direction, ref Animated animated)
        {
            if (direction.isRight)
            {
                if (playerState.isGrounded)
                {
                    if (playerState.isJumping)
                    {
                        animated.Animation.Type = AnimationType.GetAnimationTypeById(2);
                    }
                    else
                    {
                        animated.Animation.Type = AnimationType.GetAnimationTypeById(0);
                    }
                }
                else
                {
                    animated.Animation.Type = AnimationType.GetAnimationTypeById(4);
                }
            }
            else
            {
                if (playerState.isGrounded)
                {
                    if (playerState.isJumping)
                    {
                        animated.Animation.Type = AnimationType.GetAnimationTypeById(3);
                    }
                    else
                    {
                        animated.Animation.Type = AnimationType.GetAnimationTypeById(1);
                    }
                }
                else
                {
                    animated.Animation.Type = AnimationType.GetAnimationTypeById(5);
                }
            }
        }

        private static void GetDirection(ref PlayerState playerState, ref Animated animated, ref Direction direction)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right) && playerState.isGrounded)
            {
                direction.isRight = true;
            }

            if (keyboard.IsKeyDown(Keys.Left) && playerState.isGrounded)
            {
                direction.isRight = false;
            }
        }

        private static void GetJump(ref Jump jump, ref PlayerState playerState, ref Velocity velocity, ref Animated animated, ref Direction direction, GameTime gametime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Space) && playerState.isGrounded)
            {
                playerState.isJumping = true;
                if (jump.Charge < jump.Limit)
                {
                    jump.Charge += (float)gametime.ElapsedGameTime.TotalSeconds * 33;
                }
                else
                {
                    jump.Charge = jump.Limit;
                }
            }

            if (keyboard.IsKeyUp(Keys.Space) && playerState.isJumping)
            {
                playerState.isJumping = false;
                playerState.isGrounded = false;
                velocity.Value.Y = -10 * jump.Charge;
                velocity.Value.X = (10 * (jump.Charge / 2)) * (direction.isRight ? 1 : -1);
                jump.Charge = 30;
            }
        }
    }
}