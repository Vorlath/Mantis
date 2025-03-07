using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Core.MonoGame.Common
{
    /// <summary>
    /// Holds a raw frame, a bound with a name that will be applied to a spritesheet.
    /// </summary>
    public class Sprite
    {
        public string Id;
        public Rectangle Bounds;
        public Texture2D Texture;

        public Sprite(string id, Rectangle bounds, Texture2D texture)
        {
            this.Id = id;
            this.Bounds = bounds;
            this.Texture = texture;
        }
    }
}
