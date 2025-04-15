using Mantis.Example.LunarLander.Components;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Descriptors
{
    public class WallDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;

        private static readonly IComponentBuilder[] _componentsToBuild =
        [
            new ComponentBuilder<Transform2D>(new Transform2D()),
            new ComponentBuilder<Collidable>(new Collidable(0)),
            new ComponentBuilder<Size>(new Size()),
            new ComponentBuilder<Texture>(new Texture(Enums.TextureEnum.Wall, Color.White))
        ];
    }
}