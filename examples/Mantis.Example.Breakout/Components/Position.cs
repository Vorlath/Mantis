using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Components
{
    public struct Position(float x = 0, float y = 0) : IEntityComponent
    {
        public static FilterContextID filterContextID = FilterContextID.GetNewContextID();
        public Vector2 Value = new(x, y);
    }
}
