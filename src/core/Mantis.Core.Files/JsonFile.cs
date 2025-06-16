using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Files.Common;
using Mantis.Core.Serialization.Common.Services;

namespace Mantis.Core.Files
{
    public class JsonFile<T>(
        FilePath path, 
        string realPath, 
        FileStream source, 
        IJsonSerializationService jsonSerializationService
    ) : TextFile(path, realPath, source), 
        IJsonFile<T>
            where T : notnull
    {
        private readonly IJsonSerializationService _jsonSerializationService = jsonSerializationService;
        public new T Content
        {
            get
            {
                if(this._jsonSerializationService.TryDeserialize(base.Content, out T? result) == false)
                {
                    throw new NotImplementedException();
                }

                return result ?? throw new NotImplementedException();
            }
            set
            {
                if(this._jsonSerializationService.TrySerialize(value, out string? json) == false)
                {
                    throw new NotImplementedException();
                }

                base.Content = json;
            }
        }
    }
}
