using Mantis.Core.ECS.Common.Services;
using Mantis.Engine.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Console.Scenes
{
    public class BreakoutScene(ContentManager content, SpriteBatch spriteBatch, IEntityService entityService) : IScene
    {
        private readonly Texture2D _texture = content.Load<Texture2D>("Art/placeholder");
        private readonly SpriteBatch _spriteBatch = spriteBatch;

        public void Draw(GameTime gameTime)
        {
            this._spriteBatch.Begin();
            this._spriteBatch.Draw(this._texture, new Vector2(0, 0), Color.White);
            this._spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}