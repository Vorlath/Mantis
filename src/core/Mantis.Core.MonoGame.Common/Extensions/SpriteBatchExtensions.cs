using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Core.MonoGame.Common.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, GameTime gameTime, ref Animation animation, Rectangle destinationRectangle, float rotation, Color color, Vector2 origin)
        {
            var frame = animation.GetCurrentFrame(gameTime);
            //spriteBatch.Draw(frame.Texture, position, frame.Bounds, color);
            spriteBatch.Draw(frame.Texture, destinationRectangle, new Rectangle(frame.Bounds.X, frame.Bounds.Y, frame.Bounds.Width, frame.Bounds.Height), color, rotation, origin, SpriteEffects.None, 0);
        }
    }
}