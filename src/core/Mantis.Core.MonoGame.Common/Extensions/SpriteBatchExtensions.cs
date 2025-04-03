using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Core.MonoGame.Common.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, GameTime gameTime, ref Animation animation, Vector2 position, Color color)
        {
            var frame = animation.GetCurrentFrame(gameTime);
            spriteBatch.Draw(frame.Texture, position, frame.Bounds, color);
        }
    }
}
