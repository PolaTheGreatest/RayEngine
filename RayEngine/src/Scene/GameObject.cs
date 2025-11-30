using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

namespace RayEngine
{
	public class GameObject
	{
		public Vector2 Position { get; internal set; }
		public Vector2 Velocity { get; private set; }

		private float Speed;
		private float Size;

		private Color Color;

		public GameObject(Vector2 startingPos, float speed, float size, Color color)
		{
			this.Position = startingPos; 
			this.Velocity = Vector2.Zero;
			this.Speed = speed;
			this.Size = size;
			this.Color = color;
		}

		public void Update(float dt)
		{
			this.Position += this.Velocity * this.Speed * dt;
		}

		public void Draw()
		{
			Raylib.DrawCircleV(this.Position, this.Size, this.Color);
		}

		public void VelocityChange(Vector2 vel)
		{
			this.Velocity += vel;
		}

		public void ResetVelocity()
		{
			this.Velocity = Vector2.Zero;
		}
	}
}

