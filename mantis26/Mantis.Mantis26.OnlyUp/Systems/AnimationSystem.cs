using Mantis.Core.MonoGame.Common.Extensions;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.OnlyUp.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Systems
{
    internal class AnimationSystem(SpriteBatch spriteBatch) : IDrawSystem, ISceneSystem, IQueryingEntitiesEngine
    {
        private readonly SpriteBatch _spriteBatch = spriteBatch;

        public EntitiesDB entitiesDB { get; set; } = null!;

        public void Ready()
        {
            //    throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime)
        {
            var groups = this.entitiesDB.FindGroups<Animated, Transform2D, Size>();
            _ = new Vector2(512, 512);

            // Apply scaling to the sprite batch
            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            foreach (var ((animations, positions, sizes, count), _) in this.entitiesDB.QueryEntities<Animated, Transform2D, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Animated animated = ref animations[i];
                    ref Transform2D position = ref positions[i];
                    ref Size size = ref sizes[i];

                    this._spriteBatch.Draw(
                        gameTime: gameTime,
                       animation: ref animated.Animation,
                       destinationRectangle: RectangleHelper.CreateBounds(position, size),
                       rotation: position.Rotation * (MathF.PI / 180),
                       color: Color.White);
                }
            }
            this._spriteBatch.End();
        }
    }
}