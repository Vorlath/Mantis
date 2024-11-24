using Autofac;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Services;

namespace Mantis.Engine
{
    public class MantisEngine
    {
        private readonly IContainer _container;
        private ISceneService _sceneService;
        public ISceneService Scenes => _sceneService;
        public MantisEngine(Action<ContainerBuilder> customBuilder)
        {
            ContainerBuilder builder = new();
            builder.RegisterType<SceneService>().As<ISceneService>().SingleInstance();
            customBuilder(builder);
            _container = builder.Build();

            _sceneService = _container.Resolve<ISceneService>();
        }

        public void Update()
        {
            
        }

        public void Draw()
        {

        }
    }
}
