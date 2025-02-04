using Mantis.Engine.Common.Systems;
using Mantis.Example.LunarLander.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Engines
{
    public class MovementEngine(GraphicsDevice graphics) : IQueryingEntitiesEngine, IUpdateSystem, ISceneSystem
    {

        public EntitiesDB entitiesDB { get; set; } = null!;

        private readonly GraphicsDevice _graphics = graphics;

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


                    this.Update(ref velocity, ref position, ref size, ref gravity, gameTime);
                }
            }
        }

        private void Update(ref Velocity velocity, ref Transform2D position, ref Size size, ref Gravity gravity, GameTime gameTime)
        {
            velocity.Value.Y += (gravity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds);
            position.Position += (velocity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds);

            //// Check for walls
            //if (position.Position.X + size.Value.X > this._graphics.Viewport.Width)
            //{
            //    position.Position.X = 0;
            //}
            //if (position.Position.X < 0)
            //{
            //    position.Position.X = (this._graphics.Viewport.Width) - size.Value.X;
            //}
            //if (position.Position.Y + size.Value.Y > this._graphics.Viewport.Height)
            //{
            //    position.Position.Y = (this._graphics.Viewport.Height) - size.Value.Y;
            //    velocity.Value.Y *= -1;
            //}
            //if (position.Position.Y < 0)
            //{
            //    position.Position.Y = 0;
            //    velocity.Value.Y *= -1;
            //}
        }
    }
}