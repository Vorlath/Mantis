using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.Frogger.Components;
using Mantis.Mantis26.Frogger.Extensions;
using Mantis.Mantis26.Frogger.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Systems
{
    public class ControllableSystem(
        EntitiesDB entitiesDb
    ) : ISceneSystem, IUpdateSystem
    {
        private readonly EntitiesDB _entitiesDB = entitiesDb;
        private FroggerKeyboardState _last = new();

        [SequenceGroup<UpdateSequenceGroupEnum>(UpdateSequenceGroupEnum.Update)]
        public void Update(GameTime gameTime)
        {
            FroggerKeyboardState pressed = FroggerKeyboardState.GetPressed(ref this._last);

            var groups = this._entitiesDB.FindGroups<Controllable, Position>();

            foreach (var ((controllables, positions, count), _) in this._entitiesDB.QueryEntities<Controllable, Position>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    ref Controllable controllable = ref controllables[i];
                    ref Position position = ref positions[i];

                    if(controllable.TryUpdateTarget(pressed) == false && controllable.Delta == 0)
                    {
                        continue;
                    }
                    
                    //controllable.Delta += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    controllable.Delta += (float)gameTime.ElapsedGameTime.TotalSeconds * (1000f / 75f);
                    if (controllable.Delta >= 1)
                    {
                        controllable.Delta = 0;
                        position.Value = controllable.Target;
                        controllable.Origin = controllable.Target;
                    }
                    else
                    {
                        position.Value = Vector2.Lerp(controllable.Origin, controllable.Target, controllable.Delta);
                    }
                }
            }
        }
    }
}
