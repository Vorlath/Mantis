using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Mantis26.Frogger.Enums;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Components
{
    public struct Texture : IEntityComponent
    {
        public TextureEnum Value { get; set; }
    }
}
