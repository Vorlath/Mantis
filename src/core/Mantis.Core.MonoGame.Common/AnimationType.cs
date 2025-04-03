namespace Mantis.Core.MonoGame.Common
{
    //TODO getAnimationTypeByName
    public class AnimationType
    {
        public readonly int Id;
        public List<AnimationFrame> Frames;
        public AnimationType(List<AnimationFrame> frames)
        {
            this.Frames = frames;
            this.Id = _nextId++;
            _instances.Add(this.Id, this);
        }

        public static AnimationType GetAnimationTypeById(int id)
        {
            return _instances[id];
        }

        private static Dictionary<int, AnimationType> _instances = new Dictionary<int, AnimationType>();
        private static int _nextId;
    }
}
