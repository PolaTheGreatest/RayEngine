using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace RayEngine
{
    public static class Input
    {
        public static bool IsKeyDown(KeyboardKey key)
        {
            return Raylib.IsKeyDown(key);
        }
        public static bool IsKeyPressed(KeyboardKey key)
        {
            return Raylib.IsKeyPressed(key);
        }
    }
}
