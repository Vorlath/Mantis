using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct PlayerState() : IEntityComponent
    {
        public bool isGrounded { get; set; } = false;
        public bool isJumping { get; set; } = false;
        public bool isFalling { get; set; } = false;

    }
}
