using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common;
using Mantis.Core.MonoGame.Common;

namespace Mantis.Example.LunarLander
{
    public static class AnimationTypes
    {
        public static readonly Id<AnimationType> LanderIdle = Id<AnimationType>.GetByName(nameof(LanderIdle));
        public static readonly Id<AnimationType> ThrustOn = Id<AnimationType>.GetByName(nameof(ThrustOn));
        public static readonly Id<AnimationType> ThrustOff = Id<AnimationType>.GetByName(nameof(ThrustOff));
        public static readonly Id<AnimationType> RotateClockwise = Id<AnimationType>.GetByName(nameof(RotateClockwise));
        public static readonly Id<AnimationType> RotateCounterclockwise = Id<AnimationType>.GetByName(nameof(RotateCounterclockwise));
    }
}