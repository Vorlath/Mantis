using Microsoft.Xna.Framework;

namespace Mantis.Core.MonoGame.Common
{
    /// <summary>
    /// Holds a single animation, a number of frames that can be looped
    /// </summary>
    public class Animation
    {
        //private AnimatedTextureType _type;

        public float CurrentFrameDuration;
        public int CurrentFrameIndex;
        public bool Loopable;
        public bool IsPaused;
        public bool Ended;
        public AnimationType Type;


        public Animation(AnimationType type, float currentFrameDuration = 0, int currentFrameIndex = 0, bool loopable = true, bool isPaused = false)
        {
            this.CurrentFrameDuration = currentFrameDuration;
            this.CurrentFrameIndex = currentFrameIndex;
            this.Loopable = loopable;
            this.IsPaused = isPaused;
            this.Ended = false;
            this.Type = type;
        }

        public Sprite GetCurrentFrame(GameTime gameTime)
        {
            // Check if animation frame needs to be changed
            if (this.Type.Frames[this.CurrentFrameIndex].Duration <= this.CurrentFrameDuration)
            {
                this.CurrentFrameDuration = 0;
                if (++this.CurrentFrameIndex == this.Type.Frames.Length)
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
            //Console.WriteLine(this.CurrentFrameIndex);
            return this.Type.Frames[this.CurrentFrameIndex].Sprite;
        }
    }
}
