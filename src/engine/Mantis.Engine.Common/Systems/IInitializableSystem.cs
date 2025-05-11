using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;

namespace Mantis.Engine.Common.Systems
{
    public interface IInitializableSystem<in T> : ISystem
    {
        [RequireSequenceGroup<InitializeSequenceGroupEnum>]
        void Initialize(T arg);
    }
}
