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

            var groups = this.entitiesDB.FindGroups<Controllable, Velocity, Size>();
            foreach (var ((controllables, velocities, transform2Ds, _, count), _) in this.entitiesDB.QueryEntities<Controllable, Velocity, Transform2D, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Controllable controllable = ref controllables[i];
                    ref Velocity velocity = ref velocities[i];
                    ref Transform2D transform2D = ref transform2Ds[i];

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