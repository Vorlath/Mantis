using System.ComponentModel;
using Mantis.Core.Common;
using Mantis.Core.Common.Services;

namespace Mantis.Core.Services
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class MantisVariableService<TVariable>(IEnumerable<TVariable> variables) : IMantisVariableService<TVariable>
        where TVariable : IMantisVariable
    {
        private readonly Dictionary<Type, TVariable[]> _variables = variables.GroupBy(x => x.GetType())
            .ToDictionary(x => x.Key, x => x.ToArray());

        public T Get<T>()
            where T : TVariable
        {
            return (T)this._variables[typeof(T)].Last();
        }

        public IEnumerable<T> GetAll<T>()
            where T : TVariable
        {
            if (this._variables.TryGetValue(typeof(T), out TVariable[]? variables) == false)
            {
                return Enumerable.Empty<T>();
            }

            return variables.Select(x => (T)x);
        }

        public bool Has<T>()
            where T : TVariable
        {
            return this._variables.ContainsKey(typeof(T));
        }

        public bool TryGet<T>(out T? variable)
            where T : TVariable
        {
            if (this._variables.TryGetValue(typeof(T), out TVariable[]? variables) == false)
            {
                variable = default;
                return false;
            }

            if (variables.Length == 0)
            {
                variable = default;
                return false;
            }

            if (variables[variables.Length - 1] is not T casted)
            {
                variable = default;
                return false;
            }

            variable = casted;
            return true;
        }
    }
}
