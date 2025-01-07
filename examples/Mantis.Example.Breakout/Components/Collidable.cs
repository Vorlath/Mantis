using Svelto.ECS;

namespace Mantis.Example.Breakout.Components
{

    public struct Collidable(float damage) : IEntityComponent
    {
        public static FilterContextID filterContextID = FilterContextID.GetNewContextID();
        public float Damage = damage;
    }
}