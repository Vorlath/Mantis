using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;
using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common.Systems
{
    public interface IDrawSystem : ISystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        [RequireSequenceGroup<DrawSequenceGroupEnum>]
        void Draw(GameTime gameTime);
    }
}