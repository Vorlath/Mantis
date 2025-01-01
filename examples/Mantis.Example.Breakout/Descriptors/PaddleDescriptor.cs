using Mantis.Example.Breakout.Components;
using Mantis.Example.Breakout.Enums;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Descriptors
{
    public class PaddleDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;

        private static readonly IComponentBuilder[] _componentsToBuild = new IComponentBuilder[]
        {
            new ComponentBuilder<Controllable>(new Controllable()
            {
                TargetVelocity = 300f
            }),
            new ComponentBuilder<Position>(new Position()),
            new ComponentBuilder<Velocity>(new Velocity()),
            new ComponentBuilder<Size>(new Size(64, 16)),
            new ComponentBuilder<Texture>(new Texture(TextureEnum.paddle, Color.LightBlue)),
            new ComponentBuilder<Collidable>(new Collidable())
        };
    }
}
