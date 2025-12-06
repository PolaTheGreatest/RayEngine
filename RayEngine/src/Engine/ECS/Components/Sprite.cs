using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RayEngine
{
    public class Sprite : Component
    {
        public Raylib_cs.Color Color;
        public OBJECT_SHAPE Shape;

        public Sprite(Raylib_cs.Color color, OBJECT_SHAPE shape) : base("Sprite")
        {
            Color = color;
            Shape = shape;
        }
    }
}
