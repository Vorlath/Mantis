using Mantis.Core.Common;
using Microsoft.Xna.Framework;

namespace Mantis.Core.MonoGame.Common
{
    /// <summary>
    /// Holds a single animation, a number of frames that can be looped
    /// </summary>
    public struct Animation(Id<AnimationType> typeId, Color color, float currentFrameDuration = 0, int currentFrameIndex = 0, bool loopable = true, bool isPaused = false)
    {
        //private AnimatedTextureType _type;

        public float CurrentFrameDuration = currentFrameDuration;
        public int CurrentFrameIndex = currentFrameIndex;
        public bool Loopable = loopable;
        public bool IsPaused = isPaused;
        public bool Ended = false;
        public Id<AnimationType> TypeId = typeId;
        public Color Color = color;

        public Animation(AnimationType type, Color color, float currentFrameDuration = 0, int currentFrameIndex = 0, bool loopable = true, bool isPaused = false)
            : this(type.Id, color, currentFrameDuration, currentFrameIndex, loopable, isPaused)
        {
            //
        }

        public AnimationType Type
        {
            readonly get => AnimationType.GetAnimationTypeById(this.TypeId);
            set
            {
                this.TypeId = value.Id;
                this.CurrentFrameIndex = 0;
                this.CurrentFrameDuration = 0;
            }
        }

        public Sprite GetCurrentFrame(GameTime gameTime)
        {
            // Check if animation frame needs to be changed
            if (this.Type.Frames[this.CurrentFrameIndex].Duration <= this.CurrentFrameDuration)
            {
                this.CurrentFrameDuration = 0;
                if (++this.CurrentFrameIndex == this.Type.Frames.Count)
                {
                    // Check if animation needs to be looped
                    if (this.Loopable)
                    {
                        this.CurrentFrameIndex = 0;
                    }
                    else
                    {
                        // An event may need to be raised here, since the end of an animation may be important to communicate
                        this.Ended = true;
                    }
                }
            }
            else
            {
                this.CurrentFrameDuration += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            return this.Type.Frames[this.CurrentFrameIndex].Sprite;
        }
    }
}