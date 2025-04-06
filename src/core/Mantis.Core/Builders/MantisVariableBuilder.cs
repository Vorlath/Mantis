using System.Collections;
using System.ComponentModel;
using Mantis.Core.Common;
using Mantis.Core.Common.Builders;

namespace Mantis.Core.Builders
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class MantisVariableBuilder<TSelf, TVariable> : IMantisVariableBuilder<TSelf, TVariable>
        where TSelf : class, IMantisVariableBuilder<TSelf, TVariable>
        where TVariable : IMantisVariable
    {
        private readonly List<TVariable> _variables = [];

        public TSelf Add(TVariable variable)
        {
            this._variables.Add(variable);

            return this as TSelf ?? throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>()
            where T : TVariable
        {
            return this._variables.Where(x => x is T).Select(x => (T)x);
        }

        public bool TryGet<T>(out T? variable)
            where T : TVariable
        {
            variable = (T?)this._variables.LastOrDefault(x => x is T);
            return variable is not null;
        }

        public IEnumerator<TVariable> GetEnumerator()
        {
            return this._variables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}