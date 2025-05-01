using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Boundary(RectangleF boundary) : IEntityComponent
    {
        public RectangleF Value = boundary;
    }
}