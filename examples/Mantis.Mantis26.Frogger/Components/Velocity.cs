using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Components
{
    public struct Velocity : IEntityComponent
    {
        public Vector2 Value;
    }
}
