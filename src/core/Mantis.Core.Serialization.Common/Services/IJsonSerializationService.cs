using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mantis.Core.Serialization.Common.Services
{
    public interface IJsonSerializationService
    {
        bool TryDeserialize<T>(string json, [MaybeNullWhen(false)] out T result);
        bool TryDeserialize<T>(ref Utf8JsonReader reader, [MaybeNullWhen(false)] out T result);

        bool TrySerialize<T>(T value, [MaybeNullWhen(false)] out string json);
    }
}