using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common.Systems
{
    public interface IDrawSystem : ISystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        void Draw(GameTime gameTime);
    }
}