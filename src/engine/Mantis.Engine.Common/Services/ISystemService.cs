using Mantis.Engine.Common.Systems;

namespace Mantis.Engine.Common.Services
{
    public interface ISystemService<TSystem>
        where TSystem : ISystem
    {
        /// <summary>
        /// Gets all systems in the systems container
        /// </summary>
        /// <returns></returns>
        IEnumerable<TSystem> GetAll();

        /// <summary>
        /// Returns all systems of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetSystems<T>();

        /// <summary>
        /// Returns the first instance of type <typeparamref name="TSystem"/>
        /// </summary>
        /// <typeparam name="TSystem"></typeparam>
        /// <returns></returns>
        T GetSystem<T>() where T : ISystem;

        /// <summary>
        /// Determines whether or not a system of type <typeparamref name="TSystem"/> exists
        /// </summary>
        /// <typeparam name="TSystem"></typeparam>
        /// <returns></returns>
        bool HasSystem<T>() where T : ISystem;
    }

    public interface ISystemService : ISystemService<ISceneSystem>
    {

    }
}