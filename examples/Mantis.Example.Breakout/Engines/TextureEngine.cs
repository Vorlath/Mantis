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
            this._spriteBatch = spriteBatch;
            this._textures = new() {
                { TextureEnum.Ball, contentManager.Load<Texture2D>("ball") },
                { TextureEnum.Paddle, contentManager.Load<Texture2D>("paddle") },
                { TextureEnum.Block, contentManager.Load<Texture2D>("block") }
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
            this._spriteBatch.Begin();
            foreach (var ((textures, positions, sizes, count), _) in this.entitiesDB.QueryEntities<Texture, Position, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    Texture texture = textures[i];
                    Position position = positions[i];
                    Size size = sizes[i];

                    this._spriteBatch.Draw(
                        texture: this._textures[texture.Value],
                        destinationRectangle: RectangleHelper.CreateBounds(position, size),
                        color: texture.Color);
                }
            }
            this._spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}