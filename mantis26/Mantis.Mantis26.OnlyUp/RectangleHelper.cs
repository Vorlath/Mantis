using Mantis.Mantis26.OnlyUp.Components;
using Microsoft.Xna.Framework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Size = Mantis.Mantis26.OnlyUp.Components.Size;

namespace Mantis.Mantis26.OnlyUp
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

        public static RectangleF CreateBoundsF(Vector2 position, System.Drawing.SizeF size)
        {
            return new RectangleF(
                x: position.X,
                y: position.Y,
                width: size.Width,
                height: size.Height);
        }

        public static RectangleF CreateCollisionBoundsWithOffsetF(ref Transform2D position, ref Collidable collision)
        {
            return new RectangleF(
                x: position.Position.X + collision.Offset.X,
                y: position.Position.Y + collision.Offset.Y,
                width: collision.CollisionBox.Size.Width,
                height: collision.CollisionBox.Size.Height);
        }

        public static Rectangle CreateCollisionBoundsWithOffset(ref Transform2D position, ref Collidable collision)
        {
            return new Rectangle(
                x: (int)position.Position.X + (int)collision.Offset.X,
                y: (int)position.Position.Y + (int)collision.Offset.Y,
                width: (int)collision.CollisionBox.Size.Width,
                height: (int)collision.CollisionBox.Size.Height);
        }

        public static RectangleF CreateCollisionBoundsF(Vector2 position, Vector2 offset, System.Drawing.SizeF size)
        {
            return new RectangleF(
                x: position.X + offset.X,
                y: position.Y + offset.Y,
                width: size.Width,
                height: size.Height);
        }

        public static Rectangle CreateCollisionBounds(System.Drawing.PointF position, Vector2 offset, System.Drawing.SizeF size)
        {
            return new Rectangle(
                x: (int)position.X + (int)offset.X,
                y: (int)position.Y + (int)offset.Y,
                width: (int)size.Width,
                height: (int)size.Height);
        }

        public static Rectangle CreateBounds(System.Drawing.PointF position, System.Drawing.SizeF size)
        {
            return new Rectangle(
                x: (int)position.X,
                y: (int)position.Y,
                width: (int)size.Width,
                height: (int)size.Height);
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