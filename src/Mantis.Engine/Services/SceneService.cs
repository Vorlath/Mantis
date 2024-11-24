using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;

namespace Mantis.Engine.Services
{
    public sealed class SceneService : ISceneService
    {
        public SceneService()
        {

        }

        public TScene Create<TScene>()
            where TScene : IScene
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IScene> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
