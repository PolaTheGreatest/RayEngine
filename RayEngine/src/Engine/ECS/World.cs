using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RayEngine
{
    public class World
    {
        internal Dictionary<string, GameObject> Entities;
        internal Dictionary<string, System> Systems;
        internal Dictionary<GameObject, List<Component>> Components;

        public World()
        {
            Entities = [];
            Systems = [];
            Components = [];
        }

        internal void AddEntity(GameObject entity)
        {
            Entities.Add(entity.Name, entity);
        }

        internal void AddComponent<T>(GameObject entity, T component) where T : Component
        {
            // If the entity cannot be found in the database, return.
            if (!Entities.TryGetValue(entity.Name, out var Entity))
                return;

            if (Components.TryGetValue(Entity, out var List)) {
                List.Add(component);
            } 
            else
            {
                Components.Add(Entity, new List<Component> { component });
            }
        }

        internal void AddSystem(System system)
        {
            Systems.Add(system.ID, system);
        }

        internal void AddSystems(List<System> systems)
        {
            foreach (System system in systems)
            {
                AddSystem(system);
            }
        }

        internal List<GameObject> GetEntitiesWith(params string[] ComponentIDs)
        {
            List<GameObject> results = [];

            foreach (GameObject entity in Components.Keys)
            {
                List<Component> ComponentList = Components[entity];

                bool matches = true;
                foreach (string RequestedID in ComponentIDs)
                {
                    if (!ComponentList.Any(c => c.ID == RequestedID))
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                    results.Add(entity);
            }

            return results;
        }

        internal T? GetComponent<T>(GameObject entity) where T : Component
        {
            if (!Components.TryGetValue(entity, out var ComponentList))
                return null;

            foreach (Component component in ComponentList)
            {
                if (component is T typedComponent)
                    return typedComponent;
            }

            return null;
        }

        public void Emit(string Method, params object[] Arguments)
        {
            foreach (System system in Systems.Values)
            {
                var Type = system.GetType();
                var MethodInfo = Type.GetMethod(Method);

                if (MethodInfo != null)
                {
                    MethodInfo.Invoke(system, Arguments);
                }
            }
        }

        internal void Emit<T>(Action<T> call) where T : class
        {
            foreach (var system in Systems.Values)
            {
                if (system is T typedSystem)
                {
                    call(typedSystem);
                }
            }
        }

        // Invokes a function that returns a value and returns the results.
        internal List<TResult> EmitReturn<TSystem, TResult>(Func<TSystem, TResult> call) 
            where TSystem : class
        {
            List<TResult> results = [];

            foreach(var system in Systems.Values)
            {
                if (system is TSystem typedSystem)
                {
                    results.Add(call(typedSystem));
                }
            }

            return results;
        }

        // Invokes a function that returns the first value found and returns it.
        internal TResult? EmitReturnFirst<TSystem, TResult>(
            Func<TSystem, TResult> call, 
            Func<TResult, bool> stopCondition
        )
            where TSystem : class
        {
            foreach (var system in Systems.Values)
            {
                if (system is TSystem typedSystem)
                {
                    TResult result = call(typedSystem);

                    if (stopCondition(result))
                        return result;
                }
            }

            return default;
        }

        // Boolean query (return a true or false).
        internal bool EmitAny<TSystem>( Func<TSystem, bool> call ) where TSystem : class
        {
            foreach (var system in Systems.Values)
            {
                if (system is TSystem typedSystem && call(typedSystem))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
