using Mantis.Example.Breakout.Enums;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Components
{
    public struct Texture(TextureEnum value, Color color) : IEntityComponent
    {
        public TextureEnum Value = value;
        public Color Color = color;
    }
}
