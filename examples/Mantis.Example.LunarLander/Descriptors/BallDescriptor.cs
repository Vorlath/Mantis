using Mantis.Example.LunarLander.Components;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Descriptors
{
    internal class BallDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;

        private static readonly IComponentBuilder[] _componentsToBuild =
        [
            new ComponentBuilder<Position>(new Position()),
            new ComponentBuilder<Size>(new Size(10, 10)),
            new ComponentBuilder<Texture>(new Texture(Enums.TextureEnum.Lander, Color.White)),
            new ComponentBuilder<Velocity>(new Velocity())
        ];
    }
}