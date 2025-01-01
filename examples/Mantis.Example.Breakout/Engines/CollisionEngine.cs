using Mantis.Example.Breakout.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Engines
{
    public class CollisionEngine : IFrameEngine, IQueryingEntitiesEngine
    {
        public int num = 0;
        public EntitiesDB entitiesDB { get; set; }

        private readonly GraphicsDevice graphics;
        public CollisionEngine(GraphicsDevice graphics)
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
            var groups = this.entitiesDB.FindGroups<Velocity, Position, Size>(); // This should get ball, and nothing else
            foreach (var ((velocities, positions, sizes, count), _) in entitiesDB.QueryEntities<Velocity, Position, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Velocity velocity = ref velocities[i];
                    ref Position position = ref positions[i];
                    ref Size size = ref sizes[i];


                    // check for blocks
                    var blockGroups = this.entitiesDB.FindGroups<Position, Size, Collidable>();
                    foreach (var ((blockPositions, blockSizes, collidables, blockCount), _) in entitiesDB.QueryEntities<Position, Size, Collidable>(blockGroups))
                    {
                        for (int j = 0; j < count; j++)
                        {
                            ref Position blockPosition = ref blockPositions[j];
                            // ref Health blockHealth = ref healths[j];
                            ref Collidable blockCollidable = ref collidables[j];
                            ref Size blockSize = ref blockSizes[j];

                            if (RectangleHelper.CreateBounds(blockPosition, blockSize).Intersects(RectangleHelper.CreateBounds(position, size)))
                            {
                                // blockHealth.Value--;
                                // if (blockHealth.Value <= 0)
                                // {
                                //     // need to learn how to destroy stuff
                                // }
                                // find out how to bounce
                                // figuring out corners
                                float ballBottom = position.Value.Y + size.Value.Y;
                                float blockBottom = blockPosition.Value.Y + blockSize.Value.Y;
                                float ballRight = position.Value.X + size.Value.X;
                                float blockRight = blockPosition.Value.X + blockSize.Value.X;

                                float bottom = blockBottom - position.Value.Y;
                                float top = ballBottom - blockPosition.Value.Y;
                                float left = ballRight - blockPosition.Value.X;
                                float right = blockRight - position.Value.X;

                                if (top < bottom && top < left && top < right)
                                {
                                    // Top hit
                                    Console.WriteLine("Top Collision");
                                    position.Value.Y = blockPosition.Value.Y - (blockSize.Value.Y / 2) - (size.Value.Y / 2);
                                    velocity.Value.Y *= -1;
                                }
                                if (right < bottom && right < left && right < top)
                                {
                                    // Right hit
                                    Console.WriteLine("Right Collision");
                                    position.Value.X = (blockPosition.Value.X + (blockSize.Value.X / 2)) + (size.Value.X);
                                    velocity.Value.X *= -1;
                                }
                                if (bottom < top && bottom < left && bottom < right)
                                {
                                    // Bottom hit
                                    Console.WriteLine("Bottom Collision");
                                    position.Value.Y = (blockPosition.Value.Y + (blockSize.Value.Y / 2)) + (size.Value.Y / 2);
                                    velocity.Value.Y *= -1;
                                }
                                if (left < bottom && left < top && left < right)
                                {
                                    // Left hit
                                    Console.WriteLine("Left Collision");
                                    position.Value.X = blockPosition.Value.X - (blockSize.Value.X / 2);
                                    velocity.Value.X *= -1;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
