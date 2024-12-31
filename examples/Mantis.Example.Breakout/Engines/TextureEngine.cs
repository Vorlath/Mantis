using Mantis.Example.Breakout.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Engines
{
    public class TextureEngine : IFrameEngine, IQueryingEntitiesEngine
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _ball;
        GraphicsDevice graphics;

        public TextureEngine(SpriteBatch spriteBatch, ContentManager contentManager, GraphicsDevice graphics)
        {
            this.graphics = graphics;
            _spriteBatch = spriteBatch;
            _ball = contentManager.Load<Texture2D>("ball");
        }

        public EntitiesDB entitiesDB { get; set; }

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

                    _spriteBatch.Draw(_ball, new Rectangle((int)position.Value.X, (int)position.Value.Y, (int)size.Value.X, (int)size.Value.Y), Color.White);

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
