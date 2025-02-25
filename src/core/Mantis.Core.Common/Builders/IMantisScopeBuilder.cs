namespace Mantis.Core.Common.Builders
{
    public interface IMantisScopeBuilder : IMantisBuilder
    {
        IMantisRoot Root { get; }
    }
}