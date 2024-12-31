using Svelto.ECS;
using System.Numerics;

namespace Mantis.Example.Breakout.Components
{
    public struct Size(float width, float height) : IEntityComponent
    {
        public Vector2 Value = new Vector2(width, height);
    }
}
