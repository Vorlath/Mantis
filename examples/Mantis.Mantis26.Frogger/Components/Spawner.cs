using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Components
{
    public struct Spawner : IEntityComponent
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Size;
        public Vector2 Texture;
        public float Interval;
    }
}
