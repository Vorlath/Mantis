﻿using Mantis.Core.Common;
using Microsoft.Xna.Framework.Graphics;

namespace Mantis.Core.MonoGame.Common
{
    public class SpriteSheet
    {

        //Holds a spritesheet, and the different slides that have been cutout of the spritesheet

        private readonly Texture2D _texture;
        private readonly Dictionary<string, Sprite> _sprites;

        public SpriteSheet(Texture2D texture, SpriteData[] frames)
        {
            this._texture = texture;
            this._sprites = [];
            foreach (var frame in frames)
            {
                this._sprites.Add(frame.Id, new Sprite(frame.Id, frame.Bounds, this._texture));
            }
        }

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight)
        {
            throw new NotImplementedException();
        }

        public AnimationType CreateAnimationType(Id<AnimationType> id, AnimationFrameContext[] animationFrameContexts)
        {
            List<AnimationFrame> frames = [];
            foreach (AnimationFrameContext animationFrameContext in animationFrameContexts)
            {
                frames.Add(new AnimationFrame(animationFrameContext.Duration, this._sprites[animationFrameContext.Id]));
            }
            AnimationType animationType = new AnimationType(id, frames);
            return animationType;
        }
    }
}