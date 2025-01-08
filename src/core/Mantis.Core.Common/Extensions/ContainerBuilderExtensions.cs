using Autofac;

namespace Mantis.Core.Common.Extensions
{
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Register a configurator to be applied when resolving an
        /// instance of <see cref="IConfiguration{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="configurator"></param>
        /// <returns></returns>
        public static ContainerBuilder Configure<T>(this ContainerBuilder builder, Action<ILifetimeScope, T> configurator)
            where T : class, new()
        {
            builder.RegisterInstance(new Configurator<T>(configurator)).As<Configurator>();

            return builder;
        }

        /// <summary>
        /// Register a configurator to be applied when resolving an
        /// instance of <see cref="IConfiguration{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="configurator"></param>
        /// <returns></returns>
        public static ContainerBuilder Configure<T>(this ContainerBuilder builder, Action<T> configurator)
            where T : class, new()
        {
            builder.RegisterInstance(new Configurator<T>((_, i) => configurator(i))).As<Configurator>();

            return builder;
        }
    }
}
