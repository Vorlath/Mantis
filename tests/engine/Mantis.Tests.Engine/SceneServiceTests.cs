using Autofac;
using Autofac.Extras.Moq;
using Mantis.Engine.Common;
using Mantis.Engine.Services;

namespace Mantis.Tests.Engine
{
    public class SceneServiceTests
    {
        private class TestScene : IScene { }

        [Fact]
        public void SceneService_Create_UniqueInstances()
        {
            using AutoMock autoMock = AutoMock.GetLoose(builder =>
            {
                // Register test scene
                builder.RegisterType<TestScene>().InstancePerDependency();
            });

            SceneService sceneService = autoMock.Create<SceneService>();

            // Ensure there are no scenes
            Assert.Empty(sceneService.GetAll());

            // Create a single test scene
            TestScene testSceneOne = sceneService.Create<TestScene>();
            Assert.Single(sceneService.GetAll());

            // Create a second test scene
            TestScene testSceneTwo = sceneService.Create<TestScene>();
            Assert.Equal(2, sceneService.GetAll().Count());

            // Ensure both scenes are unique instances
            Assert.NotEqual(testSceneOne, testSceneTwo);
        }
    }
}