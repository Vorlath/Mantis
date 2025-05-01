using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Destructable(bool value = false) : IEntityComponent
    {
        public bool Value = value;
    }
}