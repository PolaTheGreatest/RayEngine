using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;
using System.Reflection;

namespace RayEngine
{
    public class Engine
    {
        private string? _winTitle;
        private string? _readableRenderMode;

        private Scene? gameScene1;
        private Scene? gameScene2;

        private GameObject? player1;
        private GameObject? player2;

        public SceneManager? SM;

        public World? gameWorld;

        public static List<Type> GetAllDerivedSystemTypes<TBase>()
        {
            // Look in the current assembly
            var assembly = Assembly.GetExecutingAssembly();

            // Filter all non-abstract classes inheriting TBase
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(TBase).IsAssignableFrom(t))
                .ToList();

            return types;
        }

        private void Update(float dt)
        {
            gameWorld.Emit<IUpdatable>(s => s.Update(dt, gameWorld));

            if (Input.IsKeyPressed(KeyboardKey.F))
            {
                int nextScene = (SM.currentSceneIndex == 0) ? 1 : 0;

                SM.SwitchTo(nextScene);
            }

            SM.Update(dt, gameWorld);
        }

        private void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Gray);

            gameWorld.Emit<IRenderable>(s => s.Draw(gameWorld));

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

            Raylib.EndDrawing();
        }

        private void Init()
        {
            // Simply for cosmetic display for now.
            _readableRenderMode = Config.RenderMode == RENDER_MODE.THREE_DIMENSIONAL ? "3D" : "2D";
            _winTitle = $"RayEngine {_readableRenderMode} V{Config.VersionNumber}";

            // Initialize window for Raylib
            Raylib.InitWindow(Config.ScreenWidth, Config.ScreenHeight, _winTitle);
            // Target 60 fps max for now.
            Raylib.SetTargetFPS(60);

            // Initialize the world
            gameWorld = new();

            // Get all systems deriving from the base system.
            var systemTypes = GetAllDerivedSystemTypes<System>();
            // Activator.CreateInstance(t) creates a new object of type t.
            // ! after CreateInstance is telling C# you're sure it's not null (avoid annoying non-nullable warnings.)
            var systems = systemTypes.Select(t => (System)Activator.CreateInstance(t)!).ToList();

            gameWorld.AddSystems(systems);
        }

        public void Run()
        {
            // Initialize the engine
            Init();

            // Create game objects
            player1 = new("Player1");
            gameWorld.AddEntity(player1);
            gameWorld.AddComponent<Sprite>(player1, new Sprite(
                Color.DarkGreen,
                OBJECT_SHAPE.RECT
            ));
            gameWorld.AddComponent<Transform>(player1, new Transform(
                new Vector2(300, 400),
                0.0f,
                new Vector2(10.0f, 3.0f)
            ));

            player2 = new("Player2");
            gameWorld.AddEntity(player2);
            gameWorld.AddComponent<Sprite>(player2, new Sprite(
                Color.Red,
                OBJECT_SHAPE.CIRC
            ));
            gameWorld.AddComponent<Transform>(player2, new Transform(
                new Vector2(800, 300),
                0.0f,
                new Vector2(0.0f, 3.0f)
            ));

            // Create new scenes
            gameScene1 = new(new List<GameObject>()
            {
                player1
            });
            gameScene2 = new(new List<GameObject>()
            {
                player2
            });

            // Create a SceneManager
            SM = new(gameScene1);
            SM.AddScene(gameScene2);

            // MAIN GAME LOOP
            while (!Raylib.WindowShouldClose())
            {
                float dt = Raylib.GetFrameTime();

                Update(dt);
                Draw();
            }

            // Close and unload the window.
            Raylib.CloseWindow();
        }
    }
}
