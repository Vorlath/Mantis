using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{

    public struct Collidable(float damage) : IEntityComponent
    {
        public static readonly FilterContextID FilterContextID = FilterContextID.GetNewContextID();
        public float Damage = damage;
    }
}