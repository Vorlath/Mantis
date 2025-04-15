using Mantis.Example.LunarLander.Components;
using Mantis.Example.LunarLander.Enums;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Descriptors
{
    public class LanderDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => [
            new ComponentBuilder<Transform2D>(new Transform2D()),
            new ComponentBuilder<Velocity>(new Velocity()),
            new ComponentBuilder<Size>(new Size(128, 128)),
            new ComponentBuilder<Texture>(new Texture(TextureEnum.Lander, Color.White)),
            new ComponentBuilder<Gravity>(new Gravity()),
            new ComponentBuilder<Animated>(new Animated()),
            new ComponentBuilder<Collidable>(new Collidable()),
            new ComponentBuilder<Controllable>(new Controllable()),
            ];
    }
}