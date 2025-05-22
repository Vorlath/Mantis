using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Standart.Hash.xxHash;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mantis.Core.Common
{
    /// <summary>
    /// A deterministic unique identifier that maps to a
    /// Type + string 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct Key<T> : IEquatable<Key<T>>
    {
        private readonly uint128 _value;

        private static readonly Dictionary<uint128, string> _names = [];
        private static readonly Dictionary<string, Key<T>> _ids = [];

        public readonly string Name => _names[this._value];

        private Key(string name)
        {
            this._value = xxHash128.ComputeHash(name);
            Key<T>._names.Add(this._value, name);
        }

        public static Key<T> GetByName(string name)
        {
            if (_ids.TryGetValue(name, out Key<T> id) == false)
            {
                id = new Key<T>(name);
                Key<T>._ids.Add(name, id);
            }

            return id;
        }

        #region Overrides and Equality
        public override readonly int GetHashCode()
        {
            return HashCode.Combine(this._value);
        }

        public readonly bool Equals(Key<T> other)
        {
            return this._value.high64 == other._value.high64
                && this._value.low64 == other._value.low64;
        }

        public override bool Equals(object? obj)
        {
            return obj is Key<T> key && this.Equals(key);
        }
        public static bool operator ==(Key<T> left, Key<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Key<T> left, Key<T> right)
        {
            return !(left == right);
        }
        #endregion
    }
}