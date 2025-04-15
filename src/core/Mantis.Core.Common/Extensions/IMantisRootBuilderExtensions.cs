using Autofac;
using Mantis.Core.Common.Builders;

namespace Mantis.Core.Common.Extensions
{
    public static class IMantisRootBuilderExtensions
    {
        /// <summary>
        /// Register a configurator to be applied when resolving an
        /// instance of <see cref="IConfiguration{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="configurator"></param>
        /// <returns></returns>
        public static IMantisRootBuilder Configure<T>(this IMantisRootBuilder builder, Action<ILifetimeScope, T> configurator)
            where T : class
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
        public static IMantisRootBuilder Configure<T>(this IMantisRootBuilder builder, Action<T> configurator)
            where T : class
        {
            builder.RegisterInstance(new Configurator<T>((_, i) => configurator(i))).As<Configurator>();

            return builder;
        }
    }
}