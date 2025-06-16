using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis.Core.Files.Common.Services
{
    public interface IPathService
    {
        string GetRealPath(DirectoryPath path);
        string GetRealPath(FilePath path);
    }
}
