using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Health(int value = 1) : IEntityComponent
    {
        public int Value = value;
    }
}