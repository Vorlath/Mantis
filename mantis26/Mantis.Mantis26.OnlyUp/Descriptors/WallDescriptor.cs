using Mantis.Mantis26.OnlyUp.Components;
using Mantis.Mantis26.OnlyUp.Enums;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Descriptors
{
    public class WallDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;

        private static readonly IComponentBuilder[] _componentsToBuild =
        [
            new ComponentBuilder<Transform2D>(new Transform2D()),
            new ComponentBuilder<Collidable>(new Collidable(new RectangleF(), Vector2.Zero)),
            new ComponentBuilder<Size>(new Size()),
            new ComponentBuilder<Texture>(new Texture(TextureEnum.Wall, Color.White))
        ];
    }
}