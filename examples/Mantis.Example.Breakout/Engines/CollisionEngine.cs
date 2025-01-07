using Mantis.Example.Breakout.Components;
using Mantis.Example.Breakout.Descriptors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Engines
{
    public class CollisionEngine(GraphicsDevice graphics, IEntityFunctions entityFunctions) : IFrameEngine, IQueryingEntitiesEngine
    {
        public int num = 0;
        public EntitiesDB entitiesDB { get; set; }

        private readonly GraphicsDevice graphics = graphics;
        private readonly IEntityFunctions entityFunctions = entityFunctions;

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
            foreach (var ((velocities, positions, sizes, count), _) in this.entitiesDB.QueryEntities<Velocity, Position, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Velocity velocity = ref velocities[i];
                    ref Position position = ref positions[i];
                    ref Size size = ref sizes[i];

                    RectangleF ballBounds = RectangleHelper.CreateBoundsF(position, size);

                    // check for blocks
                    var blockGroups = this.entitiesDB.FindGroups<Position, Size, Collidable, Health>();
                    foreach (var ((blockPositions, blockSizes, collidables, healths, nativeIDs, blockCount), blockGroup) in this.entitiesDB.QueryEntities<Position, Size, Collidable, Health>(blockGroups))
                    {
                        for (int j = 0; j < blockCount; j++)
                        {
                            ref Position blockPosition = ref blockPositions[j];
                            ref Health blockHealth = ref healths[j];
                            ref Collidable blockCollidable = ref collidables[j];
                            ref Size blockSize = ref blockSizes[j];

                            RectangleF blockBounds = RectangleHelper.CreateBoundsF(blockPosition, blockSize);

                            if (blockBounds.IntersectsWith(ballBounds))
                            {
                                blockHealth.Value--;

                                // https://stackoverflow.com/questions/5062833/detecting-the-direction-of-a-collision
                                float bottomIntersect = blockBounds.Bottom - ballBounds.Top;
                                float topIntersect = ballBounds.Bottom - blockBounds.Top;
                                float leftIntersect = ballBounds.Right - blockBounds.Left;
                                float rightIntersect = blockBounds.Right - ballBounds.Left;

                                if (topIntersect < bottomIntersect && topIntersect < leftIntersect && topIntersect < rightIntersect)
                                { // Top hit
                                    ballBounds.Y = blockBounds.Top - ballBounds.Height;
                                    velocity.Value.Y *= -1;
                                }
                                else if (bottomIntersect < topIntersect && bottomIntersect < leftIntersect && bottomIntersect < rightIntersect)
                                { // Bottom hit
                                    ballBounds.Y = blockBounds.Bottom;
                                    velocity.Value.Y *= -1;
                                }

                                if (rightIntersect < bottomIntersect && rightIntersect < leftIntersect && rightIntersect < topIntersect)
                                { // Right hit
                                    ballBounds.X = blockBounds.Right;
                                    velocity.Value.X *= -1;
                                }
                                else if (leftIntersect < bottomIntersect && leftIntersect < topIntersect && leftIntersect < rightIntersect)
                                { // Left hit
                                    ballBounds.X = blockBounds.Left - ballBounds.Width;
                                    velocity.Value.X *= -1;
                                }

                                if (blockHealth.Value <= 0)
                                {
                                    //var egid = new EGID(nativeIDs[j], blockGroup);
                                    this.entityFunctions.RemoveEntity<BlockDescriptor>(nativeIDs[j], blockGroup);
                                    // need to learn how to destroy stuff
                                    //blockPosition.destr
                                }
                            }
                        }
                    }

                    position.Value.X = ballBounds.Location.X;
                    position.Value.Y = ballBounds.Location.Y;
                }
            }
        }
    }
}