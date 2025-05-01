using Mantis.Core.MonoGame.Common;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Animated(AnimationType animationType) : IEntityComponent
    {
        public Animation Animation = new Animation(animationType, Microsoft.Xna.Framework.Color.White);
    }
}