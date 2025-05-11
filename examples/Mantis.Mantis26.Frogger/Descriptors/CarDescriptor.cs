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
    public class CarDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentsToBuild;
        private static readonly IComponentBuilder[] _componentsToBuild = [
            new ComponentBuilder<Texture>(new Texture(){ Value = TextureEnum.Car }),
            new ComponentBuilder<Position>(new Position() { Value = Vector2.Zero }),
            new ComponentBuilder<Size>(new Size() { Value = new Vector2(2, 1) }),
            new ComponentBuilder<Velocity>(new Velocity() { Value = new Vector2(5, 0) }),
            new ComponentBuilder<Collidable>()
        ];
    }
}
