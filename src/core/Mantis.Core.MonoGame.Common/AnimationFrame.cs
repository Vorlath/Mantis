namespace Mantis.Core.MonoGame.Common
{
    public class AnimationFrame
    {
        public readonly int Duration;
        public Sprite Sprite;

        public AnimationFrame(int duration, Sprite frame)
        {
            this.Duration = duration;
            this.Sprite = frame;
        }
    }
}
