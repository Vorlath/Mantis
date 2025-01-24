using Svelto.ECS;

namespace Mantis.Example.LunarLander.Components
{
    public struct Health(int value = 1) : IEntityComponent
    {
        public int Value = value;
    }
}