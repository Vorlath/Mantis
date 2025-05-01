using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Gravity(float gravity = 0) : IEntityComponent
    {
        public float Value = gravity;
    }
}