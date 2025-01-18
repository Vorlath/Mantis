using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common
{
    public interface IUpdateSystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime);
    }
}
