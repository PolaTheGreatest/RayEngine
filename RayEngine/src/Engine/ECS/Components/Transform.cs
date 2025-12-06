using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RayEngine
{
    public class Transform : Component
    {
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;

        public Transform(Vector2 position, float rotation, Vector2 scale) : base("Transform")
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}
