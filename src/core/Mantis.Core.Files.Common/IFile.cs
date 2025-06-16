using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis.Core.Files.Common
{
    public interface IFile : IDisposable
    {
        FilePath Path { get; }
        string RealPath { get; }
        FileStream Source { get; }

        void Save();

        void Delete();
    }
}
