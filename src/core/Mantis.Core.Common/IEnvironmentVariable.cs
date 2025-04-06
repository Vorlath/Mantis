namespace Mantis.Core.Common
{
    public interface IEnvironmentVariable : IMantisVariable
    {
    }

    public interface IEnvironmentVariable<TValue> : IMantisVariable<TValue>, IEnvironmentVariable
    {
    }

    public interface IEnvironmentVariable<TSelf, TValue> : IEnvironmentVariable<TValue>
        where TSelf : IEnvironmentVariable<TSelf, TValue>
    {
        static abstract TSelf Create(TValue value);
    }
}