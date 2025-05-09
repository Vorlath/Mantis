using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;
using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common.Systems
{
    public interface IUpdateSystem : ISystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        [RequireSequenceGroup<UpdateSequenceGroupEnum>]
        void Update(GameTime gameTime);
    }
}