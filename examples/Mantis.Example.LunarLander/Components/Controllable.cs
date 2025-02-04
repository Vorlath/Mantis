using Svelto.ECS;

namespace Mantis.Example.LunarLander.Components
{
    public struct Controllable() : IEntityComponent
    {
        public float TargetVelocity { get; set; } = 50f;

    }
}