namespace Mantis.Engine.Common.Services
{
    public interface ISystemService
    {
        /// <summary>
        /// Gets all systems in the systems container
        /// </summary>
        /// <returns></returns>
        IEnumerable<ISystem> GetAll();

        /// <summary>
        /// Returns all systems of type <typeparamref name="TSystem"/>
        /// </summary>
        /// <typeparam name="TSystem"></typeparam>
        /// <returns></returns>
        IEnumerable<TSystem> GetSystems<TSystem>();

        /// <summary>
        /// Returns the first instance of type <typeparamref name="TSystem"/>
        /// </summary>
        /// <typeparam name="TSystem"></typeparam>
        /// <returns></returns>
        ISystem GetSystem<TSystem>() where TSystem : ISystem;

        /// <summary>
        /// Determines whether or not a system of type <typeparamref name="TSystem"/> exists
        /// </summary>
        /// <typeparam name="TSystem"></typeparam>
        /// <returns></returns>
        bool HasSystem<TSystem>() where TSystem : ISystem;

    }
}
