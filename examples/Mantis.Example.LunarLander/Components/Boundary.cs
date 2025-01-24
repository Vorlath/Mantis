using Svelto.ECS;

namespace Mantis.Example.LunarLander.Components
{
    public struct Boundary(RectangleF boundary) : IEntityComponent
    {
        public RectangleF Value = boundary;
    }
}