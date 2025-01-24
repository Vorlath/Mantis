using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common.Systems
{
    public interface IUpdateSystem : ISystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime);
    }
}