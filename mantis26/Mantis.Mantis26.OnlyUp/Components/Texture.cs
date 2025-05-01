using Mantis.Mantis26.OnlyUp.Enums;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Texture(TextureEnum value, Color color) : IEntityComponent
    {
        public TextureEnum Value = value;
        public Color Color = color;
    }
}