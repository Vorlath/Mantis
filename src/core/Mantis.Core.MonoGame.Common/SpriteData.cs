
using Microsoft.Xna.Framework;

namespace Mantis.Core.MonoGame.Common
{
    /// <summary>
    /// Holds a raw frame, a bound with a name that will be applied to a spritesheet.
    /// </summary>
    public class SpriteData
    {
        public string Id;
        public Rectangle Bounds;

        public SpriteData(string id, Rectangle bounds)
        {
            this.Id = id;
            this.Bounds = bounds;
        }
    }
}
