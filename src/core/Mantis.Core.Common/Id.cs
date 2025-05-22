using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis.Core.Common
{
    /// <summary>
    /// A non deterministic unique identifier that maps to a
    /// Type + string 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Id<T> : IEquatable<Id<T>>
    {
        private readonly uint _value;

        private static uint _next = uint.MinValue;
        private static readonly Dictionary<uint, string> _names = [];
        private static readonly Dictionary<string, Id<T>> _ids = [];

        public readonly string Name => _names[this._value];

        private Id(string name)
        {
            this._value = _next++;
            Id<T>._names.Add(this._value, name);
        }

        public static Id<T> GetByName(string name)
        {
            if(_ids.TryGetValue(name, out Id<T> id) == false)
            {
                id = new Id<T>(name);
                Id<T>._ids.Add(name, id);
            }

            return id;
        }

        #region Overrides and Equality
        public override int GetHashCode()
        {
            return HashCode.Combine(this._value);
        }

        public bool Equals(Id<T> other)
        {
            return this._value == other._value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Id<T> && this.Equals((Id<T>)obj);
        }
        public static bool operator ==(Id<T> left, Id<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Id<T> left, Id<T> right)
        {
            return !(left == right);
        }
        #endregion
    }
}
