using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis.Core.Files.Common
{
    public interface IJsonFile<T> : ITextFile
    {
        new T Content { get; set; }
    }
}