using Svelto.ECS;

namespace Mantis.Example.Breakout.Components
{
    public struct Controllable() : IEntityComponent
    {
        public float TargetVelocity { get; set; } = 50f;
    }
}