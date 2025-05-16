using Mantis.Core.Common;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Services;
using Microsoft.Xna.Framework;

namespace Mantis.Engine.Common
{
    public abstract class BaseScene(ISystemService systemService) : IScene
    {
        private readonly DelegateSequenceGroup<UpdateSequenceGroupEnum, Action<GameTime>> _updateSystems = new DelegateSequenceGroup<UpdateSequenceGroupEnum, Action<GameTime>>(true);
        private readonly DelegateSequenceGroup<DrawSequenceGroupEnum, Action<GameTime>> _drawSystems = new DelegateSequenceGroup<DrawSequenceGroupEnum, Action<GameTime>>(true);

        public ISystemService SystemService { get; } = systemService;

        public virtual void Initialize()
        {
            this._updateSystems.Add(this.SystemService.GetAll());
            this._drawSystems.Add(this.SystemService.GetAll());

            DelegateSequenceGroup<InitializeSequenceGroupEnum>.Invoke(
                instances: this.SystemService.GetAll(),
                delegateType: typeof(Action<>).MakeGenericType(this.GetType()),
                sequenced: true,
                args: [this]);
        }

        public virtual void Update(GameTime gameTime)
        {
            this._updateSystems.Sequenced?.Invoke(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            this._drawSystems.Sequenced?.Invoke(gameTime);
        }
    }
}