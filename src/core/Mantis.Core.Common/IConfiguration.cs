namespace Mantis.Core.Common
{
    /// <summary>
    /// Contains an instance of type <typeparamref name="T"/> that has been
    /// configured via all configurators registered with <see cref="Extensions.ContainerBuilderExtensions.Configure{T}(Autofac.ContainerBuilder, Action{Autofac.ILifetimeScope, T})"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConfiguration<T>
        where T : class, new()
    {
        /// <summary>
        /// The configured instance.
        /// </summary>
        public T Value { get; set; }
    }
}