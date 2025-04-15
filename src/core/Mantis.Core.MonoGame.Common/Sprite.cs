using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Core.MonoGame.Common
{
    /// <summary>
    /// Holds a raw frame, a bound with a name that will be applied to a spritesheet.
    /// </summary>
    public class Sprite(string id, Rectangle bounds, Texture2D texture)
    {
        public string Id = id;
        public Rectangle Bounds = bounds;
        public Texture2D Texture = texture;
    }
}