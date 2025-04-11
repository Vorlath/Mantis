using Mantis.Core.MonoGame.Common.Extensions;
using Mantis.Engine.Common.Systems;
using Mantis.Example.LunarLander.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Systems
{
    internal class AnimationSystem : IDrawSystem, ISceneSystem, IQueryingEntitiesEngine
    {
        private SpriteBatch _spriteBatch;

        public EntitiesDB entitiesDB { get; set; } = null!;

        public void Ready()
        {
            //    throw new NotImplementedException();
        }

        public AnimationSystem(SpriteBatch spriteBatch)
        {
            this._spriteBatch = spriteBatch;
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

                    //this._spriteBatch.Draw(gameTime, ref animated.Animation, position.Position, Color.White);

                    //this._spriteBatch.Draw(
                    //    texture: this._animations[animation.Value].Animation.GetCurrentFrame(gameTime).Texture,
                    //    destinationRectangle: RectangleHelper.CreateBounds(position, size),
                    //    sourceRectangle: null,
                    //    origin: new Vector2(this._animations[animation.Value].Animation.GetCurrentFrame(gameTime).Bounds.Width / 2, this._animations[animation.Value].Animation.GetCurrentFrame(gameTime).Bounds.Height / 2),
                    //    effects: SpriteEffects.None,
                    //    layerDepth: 0,
                    //    rotation: position.Rotation * (MathF.PI / 180),
                    //    color: animation.Color);

                    //if ((int)(gameTime.TotalGameTime.TotalSeconds / 10) % 2 == 1)
                    //{
                    //    animated.Animation.Type = AnimationType.GetAnimationTypeById(1);
                    //}
                }
            }
            this._spriteBatch.End();
        }
    }
}


//internal class AnimationSystem : IDrawSystem, ISceneSystem
//{
//    private SpriteBatch _spriteBatch;

//    public AnimationSystem(SpriteBatch spriteBatch)
//    {
//        this._spriteBatch = spriteBatch;
//        //this._animation = new Animation(animationType);
//    }
//    public void Draw(GameTime gameTime)
//    {
//        this._spriteBatch.Begin();
//        this._spriteBatch.Draw(gameTime, ref this._animation, Vector2.Zero, Color.White);
//        this._spriteBatch.End();
//    }
//}
//}
