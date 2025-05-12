using Mantis.Core.Common.Attributes;
using Mantis.Engine.Common.Enums;
using Mantis.Engine.Common.Systems;
using Mantis.Mantis26.OnlyUp.Components;
using Mantis.Mantis26.OnlyUp.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Engines
{
    public class TextureEngine : IQueryingEntitiesEngine, IDrawSystem, ISceneSystem
    {
        private readonly bool _visualCollisions = false;
        private readonly SpriteBatch _spriteBatch;
        private readonly Dictionary<TextureEnum, Texture2D> _textures;

        public TextureEngine(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            this._spriteBatch = spriteBatch;
            this._textures = new() {
                { TextureEnum.Lander, contentManager.Load<Texture2D>("Lander") },
                { TextureEnum.Paddle, contentManager.Load<Texture2D>("paddle") },
                { TextureEnum.Block, contentManager.Load<Texture2D>("block") },
                { TextureEnum.Widget, contentManager.Load<Texture2D>("Widget") }
            };
        }

        public EntitiesDB entitiesDB { get; set; } = null!;

        public void Ready()
        {
            //    throw new NotImplementedException();
        }

        [SequenceGroup<DrawSequenceGroupEnum>(DrawSequenceGroupEnum.Draw)]
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
                        origin: new Vector2(0, 0),
                        effects: SpriteEffects.None,
                        layerDepth: 0,
                        rotation: position.Rotation * (MathF.PI / 180),
                        color: texture.Color);
                }
            }

            if (this._visualCollisions)
            {
                var collisionGroups = this.entitiesDB.FindGroups<Collidable, Transform2D, Size>();
                foreach (var ((collisions, positions, sizes, count), _) in this.entitiesDB.QueryEntities<Collidable, Transform2D, Size>(collisionGroups))
                {
                    for (int i = 0; i < count; i++)
                    {
                        Collidable collision = collisions[i];
                        Transform2D position = positions[i];
                        //Size size = sizes[i];

                        //Rectangle collisionBox = new Rectangle((int)collision.CollisionBox.X, (int)collision.CollisionBox.Y, (int)collision.CollisionBox.Width, (int)collision.CollisionBox.Height);
                        Rectangle collisionBox = RectangleHelper.CreateCollisionBoundsWithOffset(ref position, ref collision);

                        this._spriteBatch.Draw(
                            texture: this._textures[TextureEnum.Widget],
                            destinationRectangle: collisionBox,
                            sourceRectangle: null,
                            origin: new Vector2(0, 0),
                            effects: SpriteEffects.None,
                            layerDepth: 0,
                            rotation: position.Rotation * (MathF.PI / 180),
                            color: Color.Magenta);
                    }
                }
            }

            this._spriteBatch.End();
        }
    }
}