using Svelto.ECS;

namespace Mantis.Example.Breakout.Components
{
    public struct Collidable(float damage) : IEntityComponent
    {
        public float Damage = damage;
    }
}
