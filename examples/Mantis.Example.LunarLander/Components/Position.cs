using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Components
{
    public struct Position(float x = 0, float y = 0) : IEntityComponent
    {
        public static readonly FilterContextID FilterContextID = FilterContextID.GetNewContextID();
        public Vector2 Value = new(x, y);
    }
}