using Mantis.Core.ECS.Services;
using Mantis.Engine.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;

namespace Mantis.Console.Scenes
{
    public class BreakoutScene : IScene
    {
        private Texture2D _texture;
        private SpriteBatch _spriteBatch;
        public BreakoutScene(ContentManager content, SpriteBatch spriteBatch, EntityService entityService)
        {
            _texture = content.Load<Texture2D>("Art/placeholder");
            _spriteBatch = spriteBatch;
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, new Vector2(0,0), Color.White);
            _spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
