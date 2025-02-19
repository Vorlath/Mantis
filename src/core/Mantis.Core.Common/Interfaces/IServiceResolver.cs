namespace Mantis.Core.Common.Interfaces
{
    public interface IServiceResolver
    {
        T Resolve<T>()
            where T : class;

        object Resolve(Type type);
    }
}
