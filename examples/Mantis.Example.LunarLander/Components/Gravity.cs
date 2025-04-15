using Svelto.ECS;

namespace Mantis.Example.LunarLander.Components
{
    public struct Gravity(float gravity = 0) : IEntityComponent
    {
        public float Value = gravity;
    }
}