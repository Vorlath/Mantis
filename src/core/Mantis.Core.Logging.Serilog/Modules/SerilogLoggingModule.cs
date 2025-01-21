using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using Autofac.Core.Registration;
using Mantis.Core.Logging.Common;
using Mantis.Core.Logging.Serilog.Middleware;

namespace Mantis.Core.Logging.Serilog.Modules
{
    /// <summary>
    /// Automatically inject ILogger instances with context
    /// based on the consuming service type.
    /// 
    /// Inspired by https://github.com/nblumhardt/autofac-serilog-integration/blob/dev/src/AutofacSerilogIntegration/SerilogMiddleware.cs
    /// </summary>
    public sealed class SerilogLoggingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterGeneric(typeof(SerilogMantisLogger<>)).As(typeof(ILogger<>)).InstancePerLifetimeScope();
        }

        protected override void AttachToComponentRegistration(
            IComponentRegistryBuilder componentRegistry,
            IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);

            if (registration.Services.OfType<TypedService>().Any(ts => ts.ServiceType == typeof(ILogger)))
            {
                return;
            }

            if (registration.Activator is not ReflectionActivator ra)
            {
                return;
            }

            if (SerilogLoggingModule.UsesLogger(ra) == false)
            {
                return;
            }

            registration.PipelineBuilding += (_, pipline) =>
            {
                pipline.Use(new SerilogMantisLoggerMiddleware(registration.Activator.LimitType));
            };
        }

        private static bool UsesLogger(ReflectionActivator ra)
        {
            try
            {
                foreach (ConstructorInfo ctor in ra.ConstructorFinder.FindConstructors(ra.LimitType))
                {
                    foreach (ParameterInfo param in ctor.GetParameters())
                    {
                        if (param.ParameterType == typeof(ILogger))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (NoConstructorsFoundException)
            {
                return false;
            }
        }
    }
}