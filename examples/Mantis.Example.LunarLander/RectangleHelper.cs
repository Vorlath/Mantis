using Mantis.Example.LunarLander.Components;
using Microsoft.Xna.Framework;

namespace Mantis.Example.LunarLander
{
    public static class RectangleHelper
    {
        public static RectangleF CreateBoundsF(Transform2D position, Size size)
        {
            return new RectangleF(
                x: position.Position.X,
                y: position.Position.Y,
                width: size.Value.X,
                height: size.Value.Y);
        }

        public static Rectangle CreateBounds(Transform2D position, Size size)
        {
            return new Rectangle(
                x: (int)position.Position.X,
                y: (int)position.Position.Y,
                width: (int)size.Value.X,
                height: (int)size.Value.Y);
        }
    }
}