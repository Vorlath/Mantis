using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Mantis.Mantis26.Frogger.Utilities
{
    public struct FroggerKeyboardState
    {
        public bool Up;
        public bool Right;
        public bool Down;
        public bool Left;

        public static FroggerKeyboardState GetPressed(ref FroggerKeyboardState last)
        {
            FroggerKeyboardState current = FroggerKeyboardState.GetCurrentState();
            FroggerKeyboardState pressed = new();

            if (last.Up == false)
            {
                pressed.Up = current.Up;
            }

            if (last.Right == false)
            {
                pressed.Right = current.Right;
            }

            if (last.Down == false)
            {
                pressed.Down = current.Down;
            }

            if (last.Left == false)
            {
                pressed.Left = current.Left;
            }

            last = current;
            return pressed;
        }

        public static FroggerKeyboardState GetCurrentState()
        {
            KeyboardState state = Keyboard.GetState();

            return new FroggerKeyboardState()
            {
                Up = state.IsKeyDown(Keys.W),
                Right = state.IsKeyDown(Keys.D),
                Down = state.IsKeyDown(Keys.S),
                Left = state.IsKeyDown(Keys.A)
            };
        }
    }
}
