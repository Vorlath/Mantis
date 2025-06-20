using System.Text.Json.Serialization;
using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Extensions;

namespace Mantis.Core.Serialization.Common.Extensions
{
    public static class MantisRootBuilderExtensions
    {
        public static IMantisRootBuilder RegisterJsonConverter<TConverter>(this IMantisRootBuilder builder)
            where TConverter : JsonConverter
        {
            builder.RegisterType<TConverter>().As<JsonConverter>().SingleInstance();
            return builder;
        }
    }
}