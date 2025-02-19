using Autofac;
using Autofac.Extras.Moq;
using Mantis.Engine.Services;
using Mantis.Tests.Engine.Stubs;

namespace Mantis.Tests.Engine
{
    public class SceneServiceTests
    {
        [Fact]
        public void CreateTwoScenes_AreUnique()
        {
            using AutoMock autoMock = AutoMock.GetLoose(builder =>
            {
                // Register test scene
                builder.RegisterType<TestNotImplementedScene>().InstancePerDependency();
            });

            SceneService sceneService = autoMock.Create<SceneService>();

            // Ensure there are no scenes
            Assert.Empty(sceneService.GetAll());

            // Create a single test scene
            TestNotImplementedScene testSceneOne = sceneService.Create<TestNotImplementedScene>();
            Assert.Single(sceneService.GetAll());

            // Create a second test scene
            TestNotImplementedScene testSceneTwo = sceneService.Create<TestNotImplementedScene>();
            Assert.Equal(2, sceneService.GetAll().Count());

            // Ensure both scenes are unique instances
            Assert.NotEqual(testSceneOne, testSceneTwo);
        }
    }
}