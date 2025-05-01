using System.Numerics;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Size(float width, float height) : IEntityComponent
    {
        public Vector2 Value = new(width, height);
    }
}