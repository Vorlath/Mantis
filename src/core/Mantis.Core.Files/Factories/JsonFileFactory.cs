using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Files.Common;
using Mantis.Core.Files.Common.Factories;
using Mantis.Core.Files.Common.Services;
using Mantis.Core.Serialization.Common.Services;

namespace Mantis.Core.Files.Factories
{
    public class JsonFileFactory(IJsonSerializationService jsonSerializationService, IPathService pathService) : IFileFactory
    {
        private readonly IJsonSerializationService _jsonSerializationService = jsonSerializationService;
        private readonly IPathService _pathService = pathService;

        public bool CanCreate(Type type)
        {
            return type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IJsonFile<>);
        }

        public IFile Create(Type type, FilePath path, FileStream source)
        {
            return (IFile)(Activator.CreateInstance(typeof(JsonFile<>).MakeGenericType(type.GenericTypeArguments[0]), [
                path,
                this._pathService.GetRealPath(path),
                source,
                this._jsonSerializationService
            ]) ?? throw new NotImplementedException());
        }
    }
}
