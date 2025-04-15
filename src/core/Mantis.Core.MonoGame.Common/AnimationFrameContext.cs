namespace Mantis.Core.MonoGame.Common
{
    public readonly struct AnimationFrameContext(string id, int duration)
    {
        public string Id { get; } = id;
        public int Duration { get; } = duration;

    }
}