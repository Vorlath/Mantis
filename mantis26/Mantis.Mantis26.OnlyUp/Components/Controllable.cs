using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Controllable() : IEntityComponent
    {
        public float TargetVelocity { get; set; } = 50f;

    }
}