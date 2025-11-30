using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayEngine
{
    public class SceneManager
    {
        private List<Scene?> scenes = [];
        public int currentSceneIndex = -1;

        public SceneManager(Scene startingScene)
        {
            if (startingScene != null)
            {
                AddScene(startingScene);
            }
        }

        public void AddScene(Scene scene)
        {
            this.scenes.Add(scene);
            Console.WriteLine(this.scenes.Count);
        }

        public void SwitchTo(int index)
        {
            if (index < 0 || index >= scenes.Count)
            {
                Console.WriteLine($"Index {index} is not within the range of scene count.");
                Console.WriteLine($"Scene count is: {this.scenes.Count}.");
                return;
            }

            // unload previous scene if any
            if (currentSceneIndex != -1)
                scenes[currentSceneIndex]?.Unload();

            currentSceneIndex = index;

            scenes[currentSceneIndex]?.Load();
        }

        public void Update(float dt)
        {
            if (currentSceneIndex == -1) return;
            scenes[currentSceneIndex]?.Update(dt);
        }

        public void Draw()
        {
            if (currentSceneIndex == -1) return;
            scenes[currentSceneIndex]?.Draw();
        }
    }
}
