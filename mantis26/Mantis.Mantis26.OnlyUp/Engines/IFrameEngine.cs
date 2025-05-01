using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Engines
{
    public interface IFrameEngine : IEngine
    {
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}