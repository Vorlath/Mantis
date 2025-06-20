using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common;
using Mantis.Core.Files.Common;
using Mantis.Core.Files.Common.Factories;
using Mantis.Core.Files.Common.Services;

namespace Mantis.Core.Files.Services
{
    public class FileService(IPathService pathService, IEnumerable<IFileFactory> factories) : IFileService
    {
        private readonly IPathService _pathService = pathService;
        private readonly IFileFactory[] _factories = [.. factories];

        public IFile GetOrCreateFile(Type fileType, FilePath path)
        {
            ThrowIf.Type.IsNotAssignableFrom<IFile>(fileType);

            IFileFactory factory = this._factories.First(x => x.CanCreate(fileType));
            string realPath = this._pathService.GetRealPath(path);
            FileStream source = File.Open(realPath, FileMode.OpenOrCreate);
            IFile file = factory.Create(fileType, path, source);

            return file;
        }
    }
}