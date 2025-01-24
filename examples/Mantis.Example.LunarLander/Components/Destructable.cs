﻿using Svelto.ECS;

namespace Mantis.Example.LunarLander.Components
{
    public struct Destructable(bool value = false) : IEntityComponent
    {
        public bool Value = value;
    }
}