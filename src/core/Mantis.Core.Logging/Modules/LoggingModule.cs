using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using Autofac.Core.Registration;
using Mantis.Core.Logging.Common;
using Mantis.Core.Logging.Middleware;

namespace Mantis.Core.Logging.Modules
{
    /// <summary>
    /// Automatically inject ILogger instances with context
    /// based on the consuming service type.
    /// 
    /// Inspired by https://github.com/nblumhardt/autofac-serilog-integration/blob/dev/src/AutofacSerilogIntegration/SerilogMiddleware.cs
    /// </summary>
    internal sealed class LoggingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }

        protected override void AttachToComponentRegistration(
            IComponentRegistryBuilder componentRegistry,
            IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);

            if (registration.Services.OfType<TypedService>().Any(ts => ts.ServiceType.IsAssignableTo<ILogger>()))
            {
                return;
            }

            if (registration.Activator is not ReflectionActivator ra)
            {
                return;
            }

            this.AttachToComponentRegistration_Logger(ra, componentRegistry, registration);
        }

        private void AttachToComponentRegistration_Logger(
            ReflectionActivator ra,
            IComponentRegistryBuilder componentRegistry,
            IComponentRegistration registration)
        {
            foreach (LoggerParameterContext loggerParameterContext in LoggingModule.GetLoggerParameterContexts(ra, registration))
            {
                registration.PipelineBuilding += (_, pipline) =>
                {
                    pipline.Use(new LoggingMiddleware(loggerParameterContext));
                };
            }
        }

        private static IEnumerable<LoggerParameterContext> GetLoggerParameterContexts(ReflectionActivator ra, IComponentRegistration registration)
        {
            ConstructorInfo[] constructors = Array.Empty<ConstructorInfo>();
            try
            {
                constructors = ra.ConstructorFinder.FindConstructors(ra.LimitType);
            }
            catch (NoConstructorsFoundException)
            {
                // Not sure what to do here...
                yield break;
            }

            foreach (ConstructorInfo ctor in constructors)
            {
                foreach (ParameterInfo param in ctor.GetParameters())
                {
                    if (param.ParameterType == typeof(ILogger))
                    {
                        yield return new LoggerParameterContext(registration.Activator.LimitType, param.ParameterType);
                    }

                    if (param.ParameterType.IsAssignableTo<ILogger>() == true
                        && param.ParameterType.IsGenericType == true
                        && param.ParameterType.GetGenericTypeDefinition() == typeof(ILogger<>))
                    {
                        yield return new LoggerParameterContext(param.ParameterType.GenericTypeArguments[0], param.ParameterType);
                    }
                }
            }
        }
    }
}