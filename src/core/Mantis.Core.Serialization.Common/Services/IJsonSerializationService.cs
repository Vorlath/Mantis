using System.Diagnostics.CodeAnalysis;

namespace Mantis.Core.Serialization.Common.Services
{
    public interface IJsonSerializationService
    {
        T Deserialize<T>(string json);
        bool TryDeserialize<T>(string json, [MaybeNullWhen(false)] out T result);

        string Serialize<T>(T data);
        bool TrySerialize<T>(T data, [MaybeNullWhen(false)] out string result);
    }
}
