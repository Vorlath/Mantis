namespace Mantis.Core.Common.Interfaces
{
    public interface IServiceResolver
    {
        T ResolveService<T>()
            where T : class;

        object ResolveService(Type type);
    }
}
