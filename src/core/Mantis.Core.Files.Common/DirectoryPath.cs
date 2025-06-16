using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Files.Common.Enums;

namespace Mantis.Core.Files.Common
{
    public struct DirectoryPath(DirectoryPathTypeEnum type, string value)
    {
        public DirectoryPathTypeEnum Type { get; set; } = type;
        public string Value { get; set; } = value;

        public static DirectoryPath Absolute(string value)
        {
            return new DirectoryPath(DirectoryPathTypeEnum.Absolute, value);
        }

        public static DirectoryPath Relative(string value)
        {
            return new DirectoryPath(DirectoryPathTypeEnum.Relative, value);
        }

        public static DirectoryPath AppData(string value)
        {
            return new DirectoryPath(DirectoryPathTypeEnum.AppData, value);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is DirectoryPath path &&
                   this.Type == path.Type &&
                   this.Value == path.Value;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(this.Type, this.Value);
        }
        public static bool operator ==(DirectoryPath left, DirectoryPath right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DirectoryPath left, DirectoryPath right)
        {
            return !(left == right);
        }
    }
}