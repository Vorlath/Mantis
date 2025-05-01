using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Direction() : IEntityComponent
    {
        public bool isRight { get; set; } = true;

    }
}
