using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Jump(int limit) : IEntityComponent
    {
        public float Charge = 30;
        public int Limit = limit;
    }
}
