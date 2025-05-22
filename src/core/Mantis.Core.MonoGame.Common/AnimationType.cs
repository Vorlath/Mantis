using Mantis.Core.Common;

namespace Mantis.Core.MonoGame.Common
{
    //TODO getAnimationTypeByName
    public class AnimationType
    {
        public readonly Id<AnimationType> Id;
        public List<AnimationFrame> Frames;
        public AnimationType(Id<AnimationType> id, List<AnimationFrame> frames)
        {
            this.Frames = frames;
            this.Id = id;
            _instances.Add(this.Id, this);
        }

        public static AnimationType GetAnimationTypeById(Id<AnimationType> id)
        {
            return _instances[id];
        }

        public static int GetIdByName(string name)
        {
            if(_idsByNames.TryGetValue(name, out int id) == true)
            {
                return id;
            }
            
            id = _nextId++;
            _idsByNames.Add(name, id);

            return id;
        }

        private static readonly Dictionary<Id<AnimationType>, AnimationType> _instances = [];
        private static int _nextId;

        private static readonly Dictionary<string, int> _idsByNames = [];
    }
}