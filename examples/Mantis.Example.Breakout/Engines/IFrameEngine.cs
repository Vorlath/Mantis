using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Example.Breakout.Engines
{
    public interface IFrameEngine : IEngine
    {
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}