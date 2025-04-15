using Mantis.Engine.Common.Systems;
using Mantis.Example.LunarLander.Components;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Engines
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
            var groups = this.entitiesDB.FindGroups<Velocity, Transform2D, Size, Gravity>();
            foreach (var ((velocities, positions, sizes, gravities, count), _) in this.entitiesDB.QueryEntities<Velocity, Transform2D, Size, Gravity>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Velocity velocity = ref velocities[i];
                    ref Transform2D position = ref positions[i];
                    ref Size size = ref sizes[i];
                    ref Gravity gravity = ref gravities[i];


                    Update(ref velocity, ref position, ref gravity, gameTime);
                }
            }
        }

        private static void Update(ref Velocity velocity, ref Transform2D position, ref Gravity gravity, GameTime gameTime)
        {
            velocity.Value.Y += (gravity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds);
            position.Position += (velocity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}