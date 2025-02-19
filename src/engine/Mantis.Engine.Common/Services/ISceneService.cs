namespace Mantis.Engine.Common.Services
{
    public interface ISceneService
    {
        /// <summary>
        /// Return all active scenes
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScene> GetAll();

        /// <summary>
        /// Create a brand new scene instance
        /// </summary>
        /// <typeparam name="TScene">The type of scene to create</typeparam>
        /// <returns></returns>
        TScene Create<TScene>()
            where TScene : class, IScene;
    }
}