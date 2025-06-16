using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis.Core.Files.Common.Factories
{
    public abstract class FileFactory<TFile> : IFileFactory
        where TFile : IFile
    {
        public virtual bool CanCreate(Type type)
        {
            return typeof(TFile) == type;
        }

        IFile IFileFactory.Create(Type type, FilePath path, FileStream source)
        {
            return this.TryCreate(path, source);
        }

        protected abstract TFile TryCreate(FilePath path, FileStream source);
    }
}
