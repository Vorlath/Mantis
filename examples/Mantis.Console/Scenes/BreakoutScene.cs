using Mantis.Core.ECS.Common.Services;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Console.Scenes
{
    public class BreakoutScene(ContentManager content, SpriteBatch spriteBatch, IEntityService entityService, ISystemService systemService) : BaseScene(systemService)
    {
        private readonly Texture2D _texture = content.Load<Texture2D>("Art/placeholder");
        private readonly SpriteBatch _spriteBatch = spriteBatch;

        public void Draw(GameTime gameTime)
        {
            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);
            // Set the scale factor (e.g., 2x scaling to make the pixels appear larger)
            //var screenSize = new Vector2(512, 512);
            //var screenScale = Matrix.CreateScale(512 / screenSize.X, 512 / screenSize.Y, 2.0f);

            // Apply scaling to the sprite batch
            //spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, screenScale);
            this._spriteBatch.Draw(this._texture, new Vector2(0, 0), Color.White);
            this._spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}