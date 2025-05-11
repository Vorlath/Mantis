using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger
{
    public static class ExclusiveGroups
    {
        public static readonly ExclusiveGroup Frog = new();
        public static readonly ExclusiveGroup Car = new();
        public static readonly ExclusiveGroup Spawner = new();
    }
}
