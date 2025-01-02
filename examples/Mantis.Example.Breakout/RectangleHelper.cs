using Mantis.Example.Breakout.Components;
using Microsoft.Xna.Framework;

namespace Mantis.Example.Breakout
{
    public static class RectangleHelper
    {
        public static RectangleF CreateBoundsF(Position position, Size size)
        {
            return new RectangleF(
                x: position.Value.X,
                y: position.Value.Y,
                width: size.Value.X,
                height: size.Value.Y);
        }

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
