using Mantis.Example.LunarLander.Enums;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Components
{
    public struct Texture(TextureEnum value, Color color) : IEntityComponent
    {
        public TextureEnum Value = value;
        public Color Color = color;
    }
}