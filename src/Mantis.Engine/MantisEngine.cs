using Autofac;

namespace Mantis.Engine
{
    public class MantisEngine
    {
        private readonly IContainer _container;

        public MantisEngine(Action<ContainerBuilder> customBuilder)
        {
            ContainerBuilder builder = new();

            customBuilder(builder);

            _container = builder.Build();
        }

        public void Update()
        {
            foreach (var scene in scenes)
            {
                scene.Update();
            }
        }

        public void Draw()
        {

        }
    }
}
