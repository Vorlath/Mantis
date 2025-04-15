using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Components
{
    public struct Velocity(float x = 0, float y = 0) : IEntityComponent
    {
        public Vector2 Value = new(x, y);
    }
}