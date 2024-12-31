using Mantis.Example.Breakout.Components;
using Microsoft.Xna.Framework;

namespace Mantis.Example.Breakout
{
    public static class RectangleHelper
    {
        public static Rectangle CreateBounds(Position position, Size size)
        {
            return new Rectangle(
                x: (int)position.Value.X,
                y: (int)position.Value.Y,
                width: (int)size.Value.X,
                height: (int)size.Value.Y);
        }
    }
}
