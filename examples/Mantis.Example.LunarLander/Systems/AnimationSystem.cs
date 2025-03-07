using Mantis.Core.MonoGame.Common;
using Mantis.Core.MonoGame.Common.Extensions;
using Mantis.Engine.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Example.LunarLander.Systems
{
    internal class AnimationSystem : IDrawSystem, ISceneSystem
    {
        Texture2D Texture;
        Animation Animation;
        SpriteBatch SpriteBatch;

        public AnimationSystem(ContentManager content, SpriteBatch spriteBatch)
        {
            this.Texture = content.Load<Texture2D>("animation");
            this.SpriteBatch = spriteBatch;
            var SpriteSheet = new SpriteSheet(this.Texture, [
                new SpriteData("1", new Rectangle(0, 0, 16, 16)),
                new SpriteData("2", new Rectangle(16, 0, 16, 16)),
                new SpriteData("3", new Rectangle(0, 16, 16, 16)),
                new SpriteData("4", new Rectangle(16, 16, 16, 16))
                ]);
            var AnimationType = new AnimationType([
                new AnimationFrame(1000, SpriteSheet._sprites["1"]),
                new AnimationFrame(1000, SpriteSheet._sprites["2"]),
                new AnimationFrame(1000, SpriteSheet._sprites["3"]),
                new AnimationFrame(1000, SpriteSheet._sprites["4"])
                ]);

            this.Animation = new Animation(AnimationType);
        }
        public void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(gameTime, this.Animation, Vector2.Zero, Color.White);
            this.SpriteBatch.End();
        }
    }
}
