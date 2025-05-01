using Microsoft.Xna.Framework;
using Svelto.ECS;

namespace Mantis.Mantis26.OnlyUp.Components
{
    public struct Transform2D(float x = 0, float y = 0, float rotation = 0) : IEntityComponent
    {
        public static readonly FilterContextID FilterContextID = FilterContextID.GetNewContextID();
        public Vector2 Position = new(x, y);
        public float Rotation = rotation;
    }
}