using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace RayEngine
{
    public class Scene
    {
        // GAMEOBJECTS is what is rendered on to the screen. It is the "client-side" objects.
        private List<GameObject> gameObjects = [];
        // STOREDOBJECTS are kept in memory so when a scene is reloaded objects will reappear.
        // Note: remember to setup auto-cleanup with RemoveObject() whenever an entity is destroyed.
        private readonly List<GameObject> storedObjects = [];

        public Scene(List<GameObject>? startingObjects)
        {
            if (startingObjects != null)
            {
                storedObjects.AddRange(startingObjects);
            }
        }

        public void Load()
        {
            ClearSceneObjects();
            gameObjects.AddRange(storedObjects);
        }

        public void Unload()
        {
            ClearSceneObjects();
        }

        public void Update(float dt)
        {
            foreach (GameObject obj in gameObjects)
            {
                if (!obj.Enabled) continue;
                obj.Update(dt);
            }
        }

        public void Draw()
        {
            foreach (GameObject obj in gameObjects)
            {
                if (!obj.Enabled) continue;
                obj.Draw();
            }
        }

        public void AddObject(GameObject obj)
        {
            gameObjects.Add(obj);
            storedObjects.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            gameObjects.Remove(obj);
            storedObjects.Remove(obj);
        }

        public void ClearSceneObjects()
        {
            gameObjects.Clear();
        }

        public IReadOnlyList<GameObject> GetObjects() => gameObjects;
    }
}
