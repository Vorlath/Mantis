using Mantis.Engine.Common.Systems;
using Mantis.Example.LunarLander.Components;
using Mantis.Example.LunarLander.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Example.LunarLander.Engines
{
    public class TextureEngine : IQueryingEntitiesEngine, IDrawSystem, ISceneSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Dictionary<TextureEnum, Texture2D> _textures;

        public TextureEngine(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            this._spriteBatch = spriteBatch;
            this._textures = new() {
                { TextureEnum.Lander, contentManager.Load<Texture2D>("Lander") },
                { TextureEnum.Paddle, contentManager.Load<Texture2D>("paddle") },
                { TextureEnum.Block, contentManager.Load<Texture2D>("block") }
            };
        }

        public EntitiesDB entitiesDB { get; set; } = null!;

        public void Ready()
        {
            //    throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime)
        {
            var groups = this.entitiesDB.FindGroups<Texture, Transform2D, Size>();
            _ = new Vector2(512, 512);

            // Apply scaling to the sprite batch
            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            foreach (var ((textures, positions, sizes, count), _) in this.entitiesDB.QueryEntities<Texture, Transform2D, Size>(groups))
            {
                for (int i = 0; i < count; i++)
                {
                    Texture texture = textures[i];
                    Transform2D position = positions[i];
                    Size size = sizes[i];

                    this._spriteBatch.Draw(
                        texture: this._textures[texture.Value],
                        destinationRectangle: RectangleHelper.CreateBounds(position, size),
                        sourceRectangle: null,
                        origin: new Vector2(this._textures[texture.Value].Bounds.Width / 2, this._textures[texture.Value].Bounds.Height / 2),
                        effects: SpriteEffects.None,
                        layerDepth: 0,
                        rotation: position.Rotation * (MathF.PI / 180),
                        color: texture.Color);
                }
            }
            this._spriteBatch.End();
        }
    }
}