using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Mantis26.Frogger.Utilities
{
    public class Camera(GraphicsDevice graphics)
    {
        public static readonly float ScreenToPixelRatio = 32f;
        public static readonly float PixelToScreenRatio = 1f / ScreenToPixelRatio;

        private readonly GraphicsDevice _graphics = graphics;

        public Vector2 Position { get; set; }

        public Matrix TransformationMatrix => Matrix.CreateTranslation(
            Camera.PixelToScreen(this._graphics.Viewport.Bounds.Width / 2) + this.Position.X,
            Camera.PixelToScreen(this._graphics.Viewport.Bounds.Height / 2) + this.Position.Y, 0
        ) * Matrix.CreateScale(ScreenToPixelRatio, ScreenToPixelRatio, 1f);

        public RectangleF ScreenBounds => new RectangleF(
            this.Position.X - Camera.PixelToScreen(this._graphics.Viewport.Bounds.Width / 2),
            this.Position.Y - Camera.PixelToScreen(this._graphics.Viewport.Bounds.Height / 2),
            Camera.PixelToScreen(this._graphics.Viewport.Bounds.Width),
            Camera.PixelToScreen(this._graphics.Viewport.Bounds.Height)
        );

        public static float ScreenToPixel(float screen)
        {
            return ScreenToPixelRatio * screen;
        }

        public static float PixelToScreen(float pixel)
        {
            return PixelToScreenRatio * pixel;
        }
    }
}
