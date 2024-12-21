using Mantis.Example.Breakout.Components;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Descriptors
{
    public class WallDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;

        private static readonly IComponentBuilder[] _componentsToBuild = new IComponentBuilder[]
        {
            new ComponentBuilder<Position>(new Position()),
            new ComponentBuilder<Collidable>(new Collidable(0)),
            new ComponentBuilder<Size>(new Size()),
            new ComponentBuilder<Texture>(new Texture(Enums.TextureEnum.wall, Color.White))
        };
    }
}
