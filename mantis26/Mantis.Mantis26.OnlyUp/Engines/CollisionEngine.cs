using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.OnlyUp.Components;
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

        [SequenceGroup<UpdateSequenceGroupEnum>(UpdateSequenceGroupEnum.Update)]
        public void Update(GameTime gameTime)
        {
            var groups = this.entitiesDB.FindGroups<PlayerState, Velocity, Transform2D, Collidable>();
            foreach (var ((playerStates, velocities, positions, senderCollidables, count), group) in this.entitiesDB.QueryEntities<PlayerState, Velocity, Transform2D, Collidable>(groups))
            {
                var (senderSizes, _) = this.entitiesDB.QueryEntities<Size>(group);
                for (int i = 0; i < count; i++)
                {
                    ref Velocity velocity = ref velocities[i];
                    ref Transform2D position = ref positions[i];
                    ref Collidable senderCollidable = ref senderCollidables[i];
                    ref PlayerState playerState = ref playerStates[i];
                    ref Size senderSize = ref senderSizes[i];

                    //RectangleF senderBounds = RectangleHelper.CreateCollisionBoundsF(senderCollidable.CollisionBox.Location, senderCollidable.Offset, senderCollidable.CollisionBox.Size);
                    //RectangleF senderBounds = senderCollidable.CollisionBox;
                    RectangleF senderBounds = RectangleHelper.CreateCollisionBoundsWithOffsetF(ref position, ref senderCollidable);

                    // check for blocks
                    var blockGroups = this.entitiesDB.FindGroups<Transform2D, Collidable>();
                    foreach (var ((blockPositions, receiverCollidables, nativeIDs, blockCount), blockGroup) in this.entitiesDB.QueryEntities<Transform2D, Collidable>(blockGroups))
                    {
                        var (receiverSizes, _) = this.entitiesDB.QueryEntities<Size>(blockGroup);
                        for (int j = 0; j < blockCount; j++)
                        {
                            ref Transform2D blockPosition = ref blockPositions[j];
                            ref Collidable receiverCollidable = ref receiverCollidables[j];
                            ref Size receiverSize = ref receiverSizes[i];

                            //RectangleF receiverBounds = receiverCollidable.CollisionBox;
                            RectangleF receiverBounds = RectangleHelper.CreateCollisionBoundsWithOffsetF(ref blockPosition, ref receiverCollidable);

                            if (receiverBounds.IntersectsWith(senderBounds))
                            {
                                // https://stackoverflow.com/questions/5062833/detecting-the-direction-of-a-collision
                                float bottomIntersect = receiverBounds.Bottom - senderBounds.Top;
                                float topIntersect = senderBounds.Bottom - receiverBounds.Top;
                                float leftIntersect = senderBounds.Right - receiverBounds.Left;
                                float rightIntersect = receiverBounds.Right - senderBounds.Left;

                                if (topIntersect < bottomIntersect && topIntersect < leftIntersect && topIntersect < rightIntersect)
                                { // Top hit
                                    //MNKYBounds.Y = blockBounds.Top - MNKYBounds.Height;
                                    playerState.isGrounded = true;
                                }
                                else if (bottomIntersect < topIntersect && bottomIntersect < leftIntersect && bottomIntersect < rightIntersect)
                                { // Bottom hit
                                    position.Position.Y = receiverBounds.Bottom;
                                    velocity.Value.Y = 0;
                                    //velocity.Value.X
                                }

                                if (rightIntersect < bottomIntersect && rightIntersect < leftIntersect && rightIntersect < topIntersect)
                                { // Right hit
                                    position.Position.X = receiverCollidable.CollisionBox.Right;
                                    velocity.Value.X *= -0.5f;
                                }
                                else if (leftIntersect < bottomIntersect && leftIntersect < topIntersect && leftIntersect < rightIntersect)
                                { // Left hit
                                    position.Position.X = receiverCollidable.CollisionBox.Left - senderBounds.Size.Width - senderCollidable.Offset.X;
                                    velocity.Value.X *= -0.5f;
                                }
                            }
                        }
                    }

                    // position.Position.X = senderBounds.Location.X;
                    // position.Position.Y = senderBounds.Location.Y;
                }
            }
        }
    }
}