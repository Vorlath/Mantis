using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Engine.Common;
using Mantis.Engine.Common.Services;
using Mantis.Mantis26.Frogger.Components;
using Mantis.Mantis26.Frogger.Extensions;
using Mantis.Mantis26.Frogger.Utilities;
using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Scenes
{
    public class GameScene(ISystemService systemService, IEntityFactory entityFactory, Camera camera) : BaseScene(systemService)
    {
        private readonly IEntityFactory _entityFactory = entityFactory;
        private readonly Camera _camera = camera;

        public override void Initialize()
        {
            base.Initialize();

            this._entityFactory.BuildFrog(new Vector2(0, 6));

            this._entityFactory.BuildSpawner(new Spawner()
            {
                Position = new Vector2(this._camera.ScreenBounds.Left - 2, 4),
                Velocity = new Vector2(3, 0),
                Interval = 2f
            });


            this._entityFactory.BuildSpawner(new Spawner()
            {
                Position = new Vector2(this._camera.ScreenBounds.Left - 2, 3),
                Velocity = new Vector2(3.5f, 0),
                Interval = 2f
            });

            this._entityFactory.BuildSpawner(new Spawner()
            {
                Position = new Vector2(this._camera.ScreenBounds.Right, 1),
                Velocity = new Vector2(-3.6f, 0),
                Interval = 2f
            });
            this._entityFactory.BuildSpawner(new Spawner()
            {
                Position = new Vector2(this._camera.ScreenBounds.Left - 2, 0),
                Velocity = new Vector2(3.2f, 0),
                Interval = 2f
            });

            this._entityFactory.BuildSpawner(new Spawner()
            {
                Position = new Vector2(this._camera.ScreenBounds.Left - 2, -2),
                Velocity = new Vector2(6f, 0),
                Interval = 1f
            });
            this._entityFactory.BuildSpawner(new Spawner()
            {
                Position = new Vector2(this._camera.ScreenBounds.Left - 2, -3),
                Velocity = new Vector2(8f, 0),
                Interval = 1f
            });
            this._entityFactory.BuildSpawner(new Spawner()
            {
                Position = new Vector2(this._camera.ScreenBounds.Right, -4),
                Velocity = new Vector2(-10f, 0),
                Interval = 1f
            });
            this._entityFactory.BuildSpawner(new Spawner()
            {
                Position = new Vector2(this._camera.ScreenBounds.Right, -5),
                Velocity = new Vector2(-6f, 0),
                Interval = 1f
            });
        }
    }
}
