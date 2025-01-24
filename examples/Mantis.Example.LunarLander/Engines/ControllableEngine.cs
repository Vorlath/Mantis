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
            foreach (var ((controllables, velocities, _, count), _) in this.entitiesDB.QueryEntities<Controllable, Velocity, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Controllable controllable = ref controllables[i];
                    ref Velocity velocity = ref velocities[i];

                    velocity.Value.X = MathHelper.Lerp(velocity.Value.X, controllable.TargetVelocity * targetVelocityModifier, (float)gameTime.ElapsedGameTime.TotalSeconds * 2f);
                }
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