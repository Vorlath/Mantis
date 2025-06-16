using System.Text.Json;
using System.Text.Json.Serialization;
using Autofac;
using Mantis.Core.Common;
using Mantis.Core.Common.Builders;
using Mantis.Core.Common.Extensions;
using Mantis.Core.Files.Common.Factories;
using Mantis.Core.Files.Common.Services;
using Mantis.Core.Files.Factories;
using Mantis.Core.Files.Services;

namespace Mantis.Core.Files.Extensions
{
    public static class MantisRootBuilderExtensions
    {
        public static IMantisRootBuilder RegisterFileServices(this IMantisRootBuilder builder)
        {
            builder.RegisterType<PathService>().AsSelf().As<IPathService>().SingleInstance();
            builder.RegisterType<FileService>().AsSelf().As<IFileService>().SingleInstance();

            builder.RegisterType<JsonFileFactory>().As<IFileFactory>().SingleInstance();

            return builder;
        }
    }
}