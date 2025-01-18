namespace Mantis.Engine.Common.Services
{
    public interface ISystemService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ISystem> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSystem"></typeparam>
        /// <returns></returns>
        IEnumerable<TSystem> GetSystems<TSystem>();

        /// <summary>
        /// 
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
