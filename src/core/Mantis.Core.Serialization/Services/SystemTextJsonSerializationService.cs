using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Mantis.Core.Common;
using Mantis.Core.Logging.Common;
using Mantis.Core.Serialization.Common.Services;

namespace Mantis.Core.Serialization.Services
{
    public class SystemTextJsonSerializationService(
        IConfiguration<JsonSerializerOptions> options,
        ILogger<SystemTextJsonSerializationService> logger
    ) : IJsonSerializationService
    {
        private readonly JsonSerializerOptions _options = options.Value;
        private readonly ILogger<SystemTextJsonSerializationService> _logger = logger;

        public bool TryDeserialize<T>(string json, [MaybeNullWhen(false)] out T result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(json, this._options);
                return result is not null;
            }
            catch (Exception ex)
            {
                this._logger.Warning(ex, "Exception thrown deserializing json. Json = '{JSON}'", json);
                result = default;
                return false;
            }
        }

        public T Deserialize<T>(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json, this._options) ?? throw new ArgumentException("Invalid json deserialization result. null.");
            }
            catch (Exception ex)
            {
                this._logger.Error(ex, "Exception thrown deserializing json. Json = '{JSON}'", json);
                throw;
            }
        }

        public bool TrySerialize<T>(T data, [MaybeNullWhen(false)] out string result)
        {
            try
            {
                result = JsonSerializer.Serialize<T>(data, this._options);
                return result is not null;
            }
            catch (Exception ex)
            {
                this._logger.Warning(ex, "Exception thrown serializing json. Data = '{Data}'", data);
                result = default;
                return false;
            }
        }

        public string Serialize<T>(T data)
        {
            try
            {
                return JsonSerializer.Serialize<T>(data, this._options);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex, "Exception thrown serializing json. Data = '{Data}'", data);
                throw;
            }
        }
    }
}
