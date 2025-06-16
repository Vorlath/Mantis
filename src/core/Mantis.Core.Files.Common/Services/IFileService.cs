using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis.Core.Files.Common.Services
{
    public interface IFileService
    {
        IFile GetOrCreateFile(Type fileType, FilePath path);

        ITextFile GetOrCreateTextFile(FilePath path)
        {
            return (ITextFile)this.GetOrCreateFile(typeof(ITextFile), path);
        }

        IJsonFile<T> GetOrCreateJsonFile<T>(FilePath path)
        {
            return (IJsonFile<T>)this.GetOrCreateFile(typeof(IJsonFile<T>), path);
        }

        string ReadText(FilePath path)
        {
            using ITextFile file = this.GetOrCreateTextFile(path);
            return file.Content;
        }

        void WriteText(FilePath path, string value)
        {
            using ITextFile file = this.GetOrCreateTextFile(path);
            file.Content = value;
        }

        T ReadJson<T>(FilePath path)
        {
            using IJsonFile<T> file = this.GetOrCreateJsonFile<T>(path);
            return file.Content;
        }

        void WriteJson<T>(FilePath path, T value)
        {
            using IJsonFile<T> file = this.GetOrCreateJsonFile<T>(path);
            file.Content = value;
        }
    }
}
