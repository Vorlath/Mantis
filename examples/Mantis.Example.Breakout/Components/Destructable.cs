using Svelto.ECS;

namespace Mantis.Example.Breakout.Components
{
    public struct Destructable(bool value = false) : IEntityComponent
    {
        public bool Value = value;
    }
}
