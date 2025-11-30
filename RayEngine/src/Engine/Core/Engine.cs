using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace RayEngine
{
    public class Engine
    {
        private bool _running = true;

        private string? _winTitle;
        private string? _readableRenderMode;

        private Scene? gameScene1;
        private Scene? gameScene2;

        public SceneManager? SM;

        public GameObject player1;

        private void Update(float dt)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.F))
            {
                int sceneToSwapTo;
                // Shorthand conditional. If scene == 0, swap to 1. else, swap to 0.
                sceneToSwapTo = SM.currentSceneIndex == 0 ? 1 : 0;

                SM.SwitchTo(sceneToSwapTo);
            }

            SM.Update(dt);
        }

        private void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Gray);

            Raylib.DrawText($"Welcome to RayEngine! The current rendering mode is {_readableRenderMode}.",
                225,
                100,
                30,
                Color.Black
            );
            Raylib.DrawText($"Current scene: {SM.currentSceneIndex}",
                225,
                130,
                30,
                Color.Black);

            SM.Draw();

            Raylib.EndDrawing();
        }

        public void Run()
        {
            // Simply for cosmetic display for now.
            _readableRenderMode = Config.RenderMode == RENDER_MODE.THREE_DIMENSIONAL ? "3D" : "2D";
            _winTitle = $"RayEngine {_readableRenderMode} V0.0.1";

            // Initialize window for Raylib
            Raylib.InitWindow(Config.ScreenWidth, Config.ScreenHeight, _winTitle);
            // Target 60 fps max for now.
            Raylib.SetTargetFPS(60);

            player1 = new(
                new Transform(
                    new Vector2(350, 350),
                    45.0f,
                    new Vector2(5, 10)
                ),
                200.0f,
                Color.Blue,
                OBJECT_SHAPE.RECT
            );

            // Create new scenes
            gameScene1 = new(new List<GameObject> {
                player1
            });
            gameScene2 = new(null);

            // Create a SceneManager
            SM = new(gameScene1);
            SM.AddScene(gameScene2);

            // Swap to that scene.
            SM.SwitchTo(0);

            while (_running && (!Raylib.WindowShouldClose()))
            {
                float dt = Raylib.GetFrameTime();

                player1.Transform.Rotation += 1.0f;

                Update(dt);
                Draw();
            }

            // Close and unload the window.
            _running = false;
            Raylib.CloseWindow();
        }
    }
}
