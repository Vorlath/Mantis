using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Services;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.Frogger.Scenes;
using Microsoft.Xna.Framework;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Mantis.Mantis26.Frogger.Systems
{
    public class EngineSystem(
        EnginesRoot enginesRoot, 
        EntitiesSubmissionScheduler entitiesSubmissionScheduler
    ) : ISceneSystem, 
        IInitializableSystem<IScene>,
        IUpdateSystem
    {
        private readonly EnginesRoot _enginesRoot = enginesRoot;
        private readonly EntitiesSubmissionScheduler _entitiesSubmissionScheduler = entitiesSubmissionScheduler;

        [SequenceGroup<InitializeSequenceGroupEnum>(InitializeSequenceGroupEnum.PreInitialize)]
        public void Initialize(IScene scene)
        {
            // Automatically add all systems implementing IEngine into the EnginesRoot
            foreach (IEngine engine in scene.SystemService.GetSystems<IEngine>())
            {
                this._enginesRoot.AddEngine(engine);
            }
        }

        [SequenceGroup<UpdateSequenceGroupEnum>(UpdateSequenceGroupEnum.PreUpdate)]
        public void Update(GameTime gameTime)
        {
            // Automatically submit entity changes per frame
            this._entitiesSubmissionScheduler.SubmitEntities();
        }
    }
}
