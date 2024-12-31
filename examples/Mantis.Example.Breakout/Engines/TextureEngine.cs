using Mantis.Example.Breakout.Components;
using Mantis.Example.Breakout.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Engines
{
    public class TextureEngine : IFrameEngine, IQueryingEntitiesEngine
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Dictionary<TextureEnum, Texture2D> _textures;

        public TextureEngine(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            _spriteBatch = spriteBatch;
            _textures = new() {
                { TextureEnum.ball, contentManager.Load<Texture2D>("ball") },
                { TextureEnum.paddle, contentManager.Load<Texture2D>("paddle") }
            };
        }

        public EntitiesDB entitiesDB { get; set; } = null!;

        public void Ready()
        {
            //    throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime)
        {
            var groups = this.entitiesDB.FindGroups<Texture, Position, Size>();
            _spriteBatch.Begin();
            foreach (var ((textures, positions, sizes, count), _) in entitiesDB.QueryEntities<Texture, Position, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    Texture texture = textures[i];
                    Position position = positions[i];
                    Size size = sizes[i];

                    _spriteBatch.Draw(
                        texture: _textures[texture.Value],
                        destinationRectangle: RectangleHelper.CreateBounds(position, size),
                        color: texture.Color);
                }
            }
            _spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
