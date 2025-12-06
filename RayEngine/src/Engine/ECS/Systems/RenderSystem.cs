using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RayEngine
{
    public class RenderSystem : System, IRenderable
    {
        public override string ID { get; protected set; } = "RenderSystem";

        public override void Update(float dt, World world)
        {

        }

        public void Draw(World world)
        {
            var Sprites = world.GetEntitiesWith("Sprite", "Transform");

            foreach (var Sprite in Sprites)
            {
                if (!Sprite.Enabled) continue;

                var SpriteComp = world.GetComponent<Sprite>(Sprite);
                var TransformComp = world.GetComponent<Transform>(Sprite);

                if (SpriteComp != null && TransformComp != null)
                {
                    switch(SpriteComp.Shape)
                    {
                        case OBJECT_SHAPE.CIRC:
                            Raylib.DrawCircleV(
                                TransformComp.Position,
                                25 * TransformComp.Scale.Y,
                                SpriteComp.Color
                            );
                            break;

                        case OBJECT_SHAPE.RECT:
                            Raylib.DrawRectanglePro(
                                new Rectangle(
                                    TransformComp.Position,
                                    new Vector2(25, 25) * TransformComp.Scale
                                ),
                                new Vector2(0,0),
                                TransformComp.Rotation,
                                SpriteComp.Color
                            );
                            break;
                    }

                }
            }
        }
    }
}
