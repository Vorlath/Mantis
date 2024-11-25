using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common
{
    public interface IScene
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
