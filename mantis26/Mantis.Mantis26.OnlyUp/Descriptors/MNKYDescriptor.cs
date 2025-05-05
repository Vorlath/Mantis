using Mantis.Mantis26.OnlyUp.Components;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Descriptors
{
    public class MNKYDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => [
            new ComponentBuilder<Transform2D>(new Transform2D()),
            new ComponentBuilder<Velocity>(new Velocity()),
            new ComponentBuilder<Size>(new Size(128, 128)),
            //new ComponentBuilder<Texture>(new Texture(TextureEnum.Lander, Color.White)),
            new ComponentBuilder<Gravity>(new Gravity()),
            new ComponentBuilder<Animated>(new Animated()),
            new ComponentBuilder<Collidable>(new Collidable()),
            new ComponentBuilder<Controllable>(new Controllable()),
            new ComponentBuilder<Input>(new Input()),
            new ComponentBuilder<Direction>(new Direction()),
            new ComponentBuilder<PlayerState>(new PlayerState()),
            new ComponentBuilder<Jump>(new Jump()),
        ];
    }
}
