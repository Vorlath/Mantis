using Mantis.Engine.Common;
using Microsoft.Xna.Framework;

namespace Mantis.Tests.Engine.Stubs
{
    public class TestNotImplementedScene : IScene
    {
        public void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}