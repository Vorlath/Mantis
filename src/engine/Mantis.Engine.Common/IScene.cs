using Autofac;
using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common
{
    public interface IScene
    {
        void Initialize(ILifetimeScope scope);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}