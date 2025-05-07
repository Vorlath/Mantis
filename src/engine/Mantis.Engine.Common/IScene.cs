using Mantis.Engine.Common.Services;
using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common
{
    public interface IScene
    {
        ISystemService SystemService { get; }

        void Initialize();

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}