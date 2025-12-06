using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

namespace RayEngine
{
    public enum OBJECT_SHAPE
    {
        RECT,
		CIRC
    }

	public class GameObject(string name) : IUpdatable
	{
		public string Name { get; set; } = name;
		public bool Enabled { get; set; } = true;

		public virtual void Start() { }

		public virtual void Update(float dt, World world)
		{
		}

		public virtual void LateUpdate(float dt) { }

		public virtual void OnDestroy() { }

		public virtual void Draw()
		{
        }
	}
}

