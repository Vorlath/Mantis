using Mantis.Mantis26.OnlyUp.Components;
using Mantis.Mantis26.OnlyUp.Enums;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Descriptors
{
    internal class BallDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;

        private static readonly IComponentBuilder[] _componentsToBuild =
        [
            new ComponentBuilder<Transform2D>(new Transform2D()),
            new ComponentBuilder<Size>(new Size(10, 10)),
            new ComponentBuilder<Texture>(new Texture(TextureEnum.Lander, Color.White)),
            new ComponentBuilder<Velocity>(new Velocity())
        ];
    }
}