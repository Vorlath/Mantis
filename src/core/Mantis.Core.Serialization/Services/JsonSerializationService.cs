using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mantis.Core.Common;
using Mantis.Core.Logging.Common;
using Mantis.Core.Serialization.Common.Services;

namespace Mantis.Core.Serialization.Services
{
    public class JsonSerializationService(
        IConfiguration<JsonSerializerOptions> options, 
        ILogger logger) : IJsonSerializationService
    {
        private readonly IConfiguration<JsonSerializerOptions> _options = options;
        private readonly ILogger _logger = logger;

        public bool TryDeserialize<T>(string json, [MaybeNullWhen(false)] out T result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(json, this._options.Value) ?? throw new NotImplementedException();
                return true;
            }
            catch(Exception ex)
            {
                this._logger.Error(ex, "Exception deserializing json. Json = {JSON}", json);
                result = default;
                return false;
            }
        }

        public bool TryDeserialize<T>(ref Utf8JsonReader reader, [MaybeNullWhen(false)] out T result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(ref reader, this._options.Value) ?? throw new NotImplementedException();
                return true;
            }
            catch (Exception ex)
            {
                this._logger.Error(ex, "Exception deserializing {Utf8JsonReader}", nameof(Utf8JsonReader));
                result = default;
                return false;
            }
        }

        public bool TrySerialize<T>(T value, [MaybeNullWhen(false)] out string result)
        {
            try
            {
                result = JsonSerializer.Serialize<T>(value, this._options.Value);
                return true;
            }
            catch(Exception ex)
            {
                this._logger.Error(ex, "Exception serializing json. Type = {Type}, Value = {Value}", typeof(T).Name, value);
                result = default;
                return false;
            }
        }
    }
}
