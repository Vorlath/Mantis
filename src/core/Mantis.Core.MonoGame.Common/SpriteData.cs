
using Microsoft.Xna.Framework;

namespace Mantis.Core.MonoGame.Common
{
    /// <summary>
    /// Holds a raw frame, a bound with a name that will be applied to a spritesheet.
    /// </summary>
    public class SpriteData(string id, Rectangle bounds)
    {
        public string Id = id;
        public Rectangle Bounds = bounds;
    }
}