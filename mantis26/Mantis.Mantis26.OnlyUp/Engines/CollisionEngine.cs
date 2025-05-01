using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.OnlyUp.Components;
using Mantis.Mantis26.OnlyUp.Descriptors;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Engines
{
    public class CollisionEngine(IEntityFunctions entityFunctions) : IQueryingEntitiesEngine, IUpdateSystem, ISceneSystem
    {
        public EntitiesDB entitiesDB { get; set; } = null!;

        private readonly IEntityFunctions _entityFunctions = entityFunctions;

        public void Ready()
        {
            //    throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            var groups = this.entitiesDB.FindGroups<Velocity, Transform2D, Size>(); // This should get ball, and nothing else
            foreach (var ((velocities, positions, sizes, count), _) in this.entitiesDB.QueryEntities<Velocity, Transform2D, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Velocity velocity = ref velocities[i];
                    ref Transform2D position = ref positions[i];
                    ref Size size = ref sizes[i];

                    RectangleF ballBounds = RectangleHelper.CreateBoundsF(position, size);

                    // check for blocks
                    var blockGroups = this.entitiesDB.FindGroups<Transform2D, Size, Collidable, Health>();
                    foreach (var ((blockPositions, blockSizes, collidables, healths, nativeIDs, blockCount), blockGroup) in this.entitiesDB.QueryEntities<Transform2D, Size, Collidable, Health>(blockGroups))
                    {
                        for (int j = 0; j < blockCount; j++)
                        {
                            ref Transform2D blockPosition = ref blockPositions[j];
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
                                }
                                else if (leftIntersect < bottomIntersect && leftIntersect < topIntersect && leftIntersect < rightIntersect)
                                { // Left hit
                                    ballBounds.X = blockBounds.Left - ballBounds.Width;
                                }

                                if (blockHealth.Value <= 0)
                                {
                                    //var egid = new EGID(nativeIDs[j], blockGroup);
                                    this._entityFunctions.RemoveEntity<BlockDescriptor>(nativeIDs[j], blockGroup);
                                    // need to learn how to destroy stuff
                                    //blockPosition.destr
                                }
                            }
                        }
                    }

                    position.Position.X = ballBounds.Location.X;
                    position.Position.Y = ballBounds.Location.Y;
                }
            }
        }
    }
}