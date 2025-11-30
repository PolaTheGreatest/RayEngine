using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace RayEngine
{
    public class Renderer
    {
        public void Draw2DCircle(Vector2 Position, float Radius, Color Color)
        {
            Raylib.DrawCircleV(Position, Radius, Color);
        }
        public void Draw2DRect(Rectangle Rect, Vector2 Origin, float Rotation, Color Color)
        {
            Raylib.DrawRectanglePro(Rect, Origin, Rotation, Color);
        }
    }
}
