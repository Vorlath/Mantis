using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Mantis26.Frogger.Components;
using Mantis.Mantis26.Frogger.Enums;
using Mantis.Mantis26.Frogger.Utilities;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Descriptors
{
    public class FrogDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;
        private static readonly IComponentBuilder[] _componentsToBuild = [
            new ComponentBuilder<Texture>(new Texture(){ Value = TextureEnum.Frog }),
            new ComponentBuilder<Position>(new Position() { Value = Vector2.Zero }),
            new ComponentBuilder<Controllable>(),
            new ComponentBuilder<Size>(new Size() { Value = Vector2.One })
        ];
    }
}
