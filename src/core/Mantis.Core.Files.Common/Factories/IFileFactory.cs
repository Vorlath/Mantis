using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis.Core.Files.Common.Factories
{
    public interface IFileFactory
    {
        bool CanCreate(Type type);

        IFile Create(Type type, FilePath path, FileStream source);
    }
}
