namespace Mantis.Core.MonoGame.Common
{
    public class AnimationFrame(int duration, Sprite frame)
    {
        public readonly int Duration = duration;
        public Sprite Sprite = frame;
    }
}