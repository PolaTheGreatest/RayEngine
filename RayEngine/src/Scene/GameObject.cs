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

    public class Transform
	{
		public Vector2 Position {  get; set; } = Vector2.Zero;
		public float Rotation { get; set; } = 0f;
		public Vector2 Scale { get; set; } = new Vector2(1, 1);

		public Transform(Vector2 pos, float rot, Vector2 scale)
		{
			this.Position = pos;
			this.Rotation = rot;
			this.Scale = scale;
		}
	}

	public class GameObject
	{
		public Transform Transform { get; private set; }
		public Vector2 Velocity { get; private set; }

		private float Speed;
		private Color Color;
		private OBJECT_SHAPE Shape;

		public GameObject(Transform? initialTransform, float speed, Color color, OBJECT_SHAPE shape)
		{
			// If transform is not provided, create a new blank transform.
			this.Transform = (initialTransform != null) ? initialTransform : new(
				Vector2.Zero, 
				0f, 
				new Vector2(1, 1));
			this.Velocity = Vector2.Zero;
			this.Speed = speed;
			this.Color = color;
			this.Shape = shape;
		}

		public void Update(float dt)
		{
			this.Transform.Position += this.Velocity * this.Speed * dt;
		}

		public void Draw()
		{
			switch (this.Shape)
			{
				case OBJECT_SHAPE.CIRC:
                    Raylib.DrawCircleV(
						this.Transform.Position,
						20.0f * this.Transform.Scale.Y,
						this.Color
					);
					break;
				case OBJECT_SHAPE.RECT:
     //               Raylib.DrawRectangleV(
					//	this.Transform.Position,
					//	new Vector2(20, 20) * this.Transform.Scale,
					//	this.Color
					//);
					Raylib.DrawRectanglePro(
						new Rectangle(
							this.Transform.Position,
							new Vector2(20, 20) * this.Transform.Scale

						),
						Vector2.Zero,
						this.Transform.Rotation,
						this.Color
					);
					break;
				default:
                    Raylib.DrawRectanglePro(
                        new Rectangle(
                            this.Transform.Position,
                            new Vector2(20, 20) * this.Transform.Scale

                        ),
                        Vector2.Zero,
                        this.Transform.Rotation,
                        this.Color
                    );
                    break;
			}
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

