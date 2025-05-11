using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Mantis26.Frogger.Utilities;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Components
{
    public struct Controllable : IEntityComponent
    {
        public Vector2 Origin;
        public Vector2 Target;
        public float Delta;

        public bool TryUpdateTarget(FroggerKeyboardState pressed)
        {
            // Delta greater than zero indicates frog currently in movement
            // Do not update the target in this case
            if(this.Delta != 0)
            {
                return false;
            }

            if (pressed.Up)
            {
                this.Target.Y -= 1;
                return true;
            }

            if (pressed.Right)
            {
                this.Target.X += 1;
                return true;
            }

            if (pressed.Down)
            {
                this.Target.Y += 1;
                return true;
            }

            if (pressed.Left)
            {
                this.Target.X -= 1;
                return true;
            }

            return false;
        }
    }
}
