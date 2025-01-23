using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common
{
    public interface IMantisEngine
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}