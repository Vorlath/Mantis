using Mantis.Mantis26.OnlyUp.Components;
using Mantis.Mantis26.OnlyUp.Enums;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Descriptors
{
    public class PaddleDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;

        private static readonly IComponentBuilder[] _componentsToBuild =
        [
            new ComponentBuilder<Controllable>(new Controllable()
            {
                TargetVelocity = 300f
            }),
            new ComponentBuilder<Transform2D>(new Transform2D()),
            new ComponentBuilder<Velocity>(new Velocity()),
            new ComponentBuilder<Size>(new Size(64, 16)),
            new ComponentBuilder<Texture>(new Texture(TextureEnum.Paddle, Color.LightBlue)),
            new ComponentBuilder<Collidable>(new Collidable()),
            new ComponentBuilder<Health>(new Health(10000000))

        ];
    }
}