using System.Numerics;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Components
{
    public struct Size(float width, float height) : IEntityComponent
    {
        public Vector2 Value = new(width, height);
    }
}