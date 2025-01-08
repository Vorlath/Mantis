using System.ComponentModel;
using Autofac;

namespace Mantis.Core.Common
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class Configurator
    {
        public abstract bool TryConfigureInstance(ILifetimeScope scope, object instance);
    }

    /// <summary>
    /// Contains a configurator method to be applied to type
    /// <typeparamref name="T"/> when constructing a configurated
    /// instance.
    /// 
    /// For internal use only, automatically generated via
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configurator"></param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Configurator<T>(Action<ILifetimeScope, T> configurator) : Configurator
        where T : class
    {
        private readonly Action<ILifetimeScope, T> _configurator = configurator;

        public override bool TryConfigureInstance(ILifetimeScope scope, object instance)
        {
            if (instance is not T casted)
            {
                return false;
            }

            this._configurator(scope, casted);
            return true;
        }
    }
}
