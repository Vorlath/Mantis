using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantis.Core.Common.Attributes;
using Mantis.Core.MonoGame.Common.Extensions;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.Frogger.Components;
using Mantis.Mantis26.Frogger.Enums;
using Mantis.Mantis26.Frogger.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Mantis26.Frogger.Systems
{
    public class TextureSystem(
        SpriteBatch spriteBatch, 
        ContentManager contentManager,
        EntitiesDB entitiesDb,
        Camera camera) : ISceneSystem, IDrawSystem
    {
        private readonly Camera _camera = camera;
        private readonly EntitiesDB _entitiesDB = entitiesDb;
        private readonly SpriteBatch _spriteBatch = spriteBatch;
        private readonly Dictionary<TextureEnum, Texture2D> _textures = new() {
            { TextureEnum.Frog, contentManager.Load<Texture2D>("Sprites/frog") },
            { TextureEnum.Car, contentManager.Load<Texture2D>("Sprites/car") },
        };

        [SequenceGroup<DrawSequenceGroupEnum>(DrawSequenceGroupEnum.Draw)]
        public void Draw(GameTime gameTime)
        {
            var groups = this._entitiesDB.FindGroups<Texture, Position, Size>();

            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, this._camera.TransformationMatrix);
            foreach (var ((textures, positions, sizes, count), _) in this._entitiesDB.QueryEntities<Texture, Position, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    Texture texture = textures[i];
                    Position position = positions[i];
                    Size size = sizes[i];

                    this._spriteBatch.Draw(
                        this._textures[texture.Value],
                        position.Value,
                        this._textures[texture.Value].Bounds,
                        Color.White,
                        0f,
                        Vector2.Zero,
                        Camera.PixelToScreenRatio,
                        SpriteEffects.None, 
                        0);
                }
            }
            this._spriteBatch.End();
        }
    }
}
