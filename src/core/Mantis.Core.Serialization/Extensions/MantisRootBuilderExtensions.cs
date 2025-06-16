using System.Text.Json;
using System.Text.Json.Serialization;
using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Serialization.Common.Services;
using Mantis.Core.Serialization.Services;

namespace Mantis.Core.Serialization.Extensions
{
    public static class MantisRootBuilderExtensions
    {
        public static IMantisRootBuilder RegisterJsonServices(this IMantisRootBuilder builder)
        {
            builder.RegisterType<JsonSerializationService>().AsSelf().As<IJsonSerializationService>().SingleInstance();
            builder.Configure<JsonSerializerOptions>((scope, options) =>
            {
                options.PropertyNameCaseInsensitive = true;
                options.WriteIndented = true;
                options.Converters.Add(new JsonStringEnumConverter());

                // Ensure all registered JsonConverters are applied to the global JsonSerializationOptions
                foreach (JsonConverter converter in scope.Resolve<IEnumerable<JsonConverter>>())
                {
                    options.Converters.Add(converter);
                }
            });

            return builder;
        }
    }
}