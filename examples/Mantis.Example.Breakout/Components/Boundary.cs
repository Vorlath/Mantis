using System.Drawing;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Components
{
    public struct Boundary(RectangleF boundary) : IEntityComponent
    {
        public RectangleF Value = boundary;
    }
}