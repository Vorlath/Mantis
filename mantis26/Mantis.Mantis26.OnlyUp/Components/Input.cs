using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Input() : IEntityComponent
    {
        public bool isChargingJump { get; set; } = false;

    }
}
