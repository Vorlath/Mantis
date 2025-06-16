using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Files.Common.Enums;

namespace Mantis.Core.Files.Common
{
    public struct FilePath(DirectoryPath directory, string name)
    {
        public DirectoryPath Directory { get; set; } = directory;
        public string Name { get; set; } = name;
        public readonly string Value => Path.Combine(this.Directory.Value, this.Name);

        public static FilePath Absolute(string value)
        {
            string directory = Path.GetDirectoryName(value) ?? throw new NotImplementedException();
            string name = Path.GetFileName(value);

            return new FilePath(new DirectoryPath(DirectoryPathTypeEnum.Absolute, directory), name);
        }

        public static FilePath Relative(string value)
        {
            string directory = Path.GetDirectoryName(value) ?? throw new NotImplementedException();
            string name = Path.GetFileName(value);

            return new FilePath(new DirectoryPath(DirectoryPathTypeEnum.Relative, directory), name);
        }

        public static FilePath AppData(string value)
        {
            string directory = Path.GetDirectoryName(value) ?? throw new NotImplementedException();
            string name = Path.GetFileName(value);

            return new FilePath(new DirectoryPath(DirectoryPathTypeEnum.AppData, directory), name);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is FilePath path &&
                   EqualityComparer<DirectoryPath>.Default.Equals(this.Directory, path.Directory) &&
                   this.Name == path.Name;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(this.Directory, this.Name);
        }
        public static bool operator ==(FilePath left, FilePath right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FilePath left, FilePath right)
        {
            return !(left == right);
        }
    }
}