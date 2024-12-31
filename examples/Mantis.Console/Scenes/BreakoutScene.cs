using Mantis.Core.ECS.Common.Services;
using Mantis.Engine.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Console.Scenes
{
    public class BreakoutScene : IScene
    {
        private readonly Texture2D _texture;
        private readonly SpriteBatch _spriteBatch;
        public BreakoutScene(ContentManager content, SpriteBatch spriteBatch, IEntityService entityService)
        {
            _texture = content.Load<Texture2D>("Art/placeholder");
            _spriteBatch = spriteBatch;
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, new Vector2(0, 0), Color.White);
            _spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
