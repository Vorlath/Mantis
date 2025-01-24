using Svelto.ECS;

namespace Mantis.Example.LunarLander.Components
{

    public struct Collidable(float damage) : IEntityComponent
    {
        public static readonly FilterContextID FilterContextID = FilterContextID.GetNewContextID();
        public float Damage = damage;
    }
}