namespace Mantis.Core.Common.Implementations
{
    public abstract class EnvironmentVariable<TSelf, TValue>(TValue value) : IEnvironmentVariable<TValue>
        where TSelf : EnvironmentVariable<TSelf, TValue>
    {
        public TValue Value { get; } = value;

        public static TSelf Create(TValue value)
        {
            object? variable = Activator.CreateInstance(typeof(TSelf), [value]) ?? throw new NotImplementedException();
            if (variable is not TSelf casted)
            {
                throw new NotImplementedException();
            }

            return casted;
        }
    }
}