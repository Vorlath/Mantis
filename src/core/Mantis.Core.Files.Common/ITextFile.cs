using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis.Core.Files.Common
{
    public interface ITextFile : IFile
    {
        Encoding Encoding { get; set; }
        string Content { get; set; }
    }
}
