namespace Mantis.Core.Common.Services
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Apply registered configurators to the given instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void Configure<T>(T instance)
            where T : class;

        /// <summary>
        /// Create a new instance and apply registered configurators to it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Configure<T>()
            where T : class, new()
        {
            T instance = new();
            this.Configure<T>(instance);
            return instance;
        }
    }
}
