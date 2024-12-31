using Mantis.Example.Breakout.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Engines
{
    public class TextureEngine : IFrameEngine, IQueryingEntitiesEngine
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _ball;

        public TextureEngine(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            _spriteBatch = spriteBatch;
            _ball = contentManager.Load<Texture2D>("ball");
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
                        texture: _ball,
                        destinationRectangle: RectangleHelper.CreateBounds(position, size),
                        color: Color.White);
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
