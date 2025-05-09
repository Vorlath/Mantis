namespace Mantis.Core.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class RequireGenericSequenceGroupAttribute(string genericArgumentName) : Attribute
    {
        public readonly string GenericArgumentName = genericArgumentName;
    }
}