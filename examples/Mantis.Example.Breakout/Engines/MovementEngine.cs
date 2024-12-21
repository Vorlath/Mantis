using Mantis.Example.Breakout.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Engines
{
    public class MovementEngine : IFrameEngine, IQueryingEntitiesEngine
    {

        public EntitiesDB entitiesDB { get; set; }
        GraphicsDevice graphics;
        public MovementEngine(GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }

        public void Ready()
        {
            //    throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime)
        {

        }

        public void Update(GameTime gameTime)
        {
            var groups = this.entitiesDB.FindGroups<Velocity, Position>();
            foreach (var ((velocities, positions, count), _) in entitiesDB.QueryEntities<Velocity, Position>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Velocity velocity = ref velocities[i];
                    ref Position position = ref positions[i];

                    this.Update(ref velocity, ref position, gameTime);
                }
            }
        }

        private void Update(ref Velocity velocity, ref Position position, GameTime gameTime)
        {

            position.Value += (velocity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (position.Value.X > this.graphics.Viewport.Width)
            {
                position.Value.X = this.graphics.Viewport.Width;
                velocity.Value.X *= -1;
            }
            if (position.Value.X < 0)
            {
                position.Value.X = 0;
                velocity.Value.X *= -1;
            }
            if (position.Value.Y > this.graphics.Viewport.Height)
            {
                position.Value.Y = this.graphics.Viewport.Height;
                velocity.Value.Y *= -1;
            }
            if (position.Value.Y < 0)
            {
                position.Value.Y = 0;
                velocity.Value.Y *= -1;
            }
        }
    }
}
