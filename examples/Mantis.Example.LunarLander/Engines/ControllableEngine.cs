using Mantis.Core.MonoGame.Common;
using Mantis.Engine.Common.Systems;
using Mantis.Example.LunarLander.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Engines
{
    internal class ControllableEngine : IEngine, IQueryingEntitiesEngine, IUpdateSystem, ISceneSystem
    {
        public EntitiesDB entitiesDB { get; set; } = null!;

        public void Ready()
        {
            // throw new NotImplementedException();
        }


        public void Update(GameTime gameTime)
        {
            float targetVelocityModifier = GetTargetVelocityModifier();

            var groups = this.entitiesDB.FindGroups<Animated, Controllable, Velocity>();
            foreach (var ((animations, controllables, velocities, transform2Ds, _, count), group) in this.entitiesDB.QueryEntities<Animated, Controllable, Velocity, Transform2D>(groups))
            {
                var (textures, _) = this.entitiesDB.QueryEntities<Texture>(group);
                for (int i = 0; i < count; i++)
                {
                    ref Controllable controllable = ref controllables[i];
                    ref Velocity velocity = ref velocities[i];
                    ref Transform2D transform2D = ref transform2Ds[i];
                    ref Animated animated = ref animations[i];
                    ref Texture texture = ref textures[i];

                    if (GetThrusters() == 1)
                    {
                        if (animated.Animation.TypeId != 1)
                        {
                            animated.Animation.Type = AnimationType.GetAnimationTypeById(1);
                        }
                    }
                    else
                    {
                        if (animated.Animation.TypeId != 0)
                        {
                            animated.Animation.Type = AnimationType.GetAnimationTypeById(0);
                        }
                    }

                    transform2D.Rotation += (100f * targetVelocityModifier) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    velocity.Value += new Vector2(MathF.Sin(transform2D.Rotation * (MathF.PI / 180)), -MathF.Cos(transform2D.Rotation * (MathF.PI / 180))) * (GetThrusters() * 0.0012f);
                    //velocity.Value.X = MathHelper.Lerp(velocity.Value.X, controllable.TargetVelocity * targetVelocityModifier, (float)gameTime.ElapsedGameTime.TotalSeconds * 2f);
                }
            }
        }

        private static int GetThrusters()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private static float GetTargetVelocityModifier()
        {
            KeyboardState keyboard = Keyboard.GetState();
            bool isLeftActive = keyboard.IsKeyDown(Keys.A);
            bool isRightActive = keyboard.IsKeyDown(Keys.D);

            if (isRightActive && isLeftActive)
            {
                return 0;
            }

            if (isRightActive)
            {
                return 1;
            }

            if (isLeftActive)
            {
                return -1;
            }

            return 0;
        }
    }
}