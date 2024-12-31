using Svelto.ECS;
using System.Drawing;

namespace Mantis.Example.Breakout.Components
{
    public struct Boundary(RectangleF boundary) : IEntityComponent
    {
        public RectangleF Value = boundary;
    }
}
