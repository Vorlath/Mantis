using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Features.OpenGenerics;
using Mantis.Core.Common.Builders;

namespace Mantis.Core.Common.Extensions
{
    public static class IMantisBuilderExtensions
    {
        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> RegisterInstance<T>(
            this IMantisBuilder builder,
            T instance)
                where T : class
        {
            return builder.ContainerBuilder.RegisterInstance(instance);
        }

        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> Register<T>(
            this IMantisBuilder builder,
            Func<IComponentContext, T> factory)
                where T : class
        {
            return builder.ContainerBuilder.Register(factory);
        }

        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> Register<T>(
            this IMantisBuilder builder,
            Func<ILifetimeScope, T> factory)
                where T : class
        {
            return builder.ContainerBuilder.Register(factory);
        }

        public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterType<T>(
            this IMantisBuilder builder)
                where T : notnull
        {
            return builder.ContainerBuilder.RegisterType<T>();
        }

        public static IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterType(
            this IMantisBuilder builder,
            Type implementationType)
        {
            return builder.ContainerBuilder.RegisterType(implementationType);
        }

        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> RegisterGeneric(
            this IMantisBuilder builder,
            Type implementor)
        {
            return builder.ContainerBuilder.RegisterGeneric(implementor);
        }

        public static IRegistrationBuilder<object, OpenGenericDelegateActivatorData, DynamicRegistrationStyle> RegisterGeneric(
            this IMantisBuilder builder,
            Func<IComponentContext, Type[], object> factory)
        {
            return builder.ContainerBuilder.RegisterGeneric(factory);
        }

        public static IModuleRegistrar RegisterModule<T>(
            this IMantisBuilder builder)
                where T : IModule, new()
        {
            return builder.ContainerBuilder.RegisterModule<T>();
        }
    }
}
