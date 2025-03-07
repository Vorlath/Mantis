using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Core.MonoGame.Common
{
    public class SpriteSheet
    {

        //Holds a spritesheet, and the different slides that have been cutout of the spritesheet

        Texture2D _texture;
        public readonly Dictionary<string, Sprite> _sprites;

        public SpriteSheet(Texture2D texture, SpriteData[] frames)
        {
            _texture = texture;
            this._sprites = new Dictionary<string, Sprite>();
            foreach (var frame in frames)
            {
                _sprites.Add(frame.Id, new Sprite(frame.Id, frame.Bounds, _texture));
            }
        }

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight)
        {
            throw new NotImplementedException();
        }

        //public AnimatedTextureType CreateAnimatedTextureType(params string[] frames)
        //{
        //    return new AnimatedTextureType(_texture, frames.Select(x => _frames[x].ToArray()));
        //}
    }
}
