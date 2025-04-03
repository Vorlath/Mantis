namespace Mantis.Core.MonoGame.Common
{
    public struct AnimationFrameContext
    {
        public AnimationFrameContext(string id, int duration)
        {
            this.Id = id;
            this.Duration = duration;
        }
        public string Id { get; }
        public int Duration { get; }

    }
}
