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
		public Vector2 Velocity { get; protected set; }

		private float Speed;
		private Color Color;
		private OBJECT_SHAPE Shape;

		public bool Enabled { get; set; } = true;

		public string Tag { get; set; } = "Untagged";
		public int Layer { get; set; } = 0;

		public Renderer Renderer = new();

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

		public virtual void Start() { }

		public virtual void Update(float dt)
		{
			this.Transform.Position += this.Velocity * this.Speed * dt;
		}

		public virtual void LateUpdate(float dt) { }

		public virtual void OnDestroy() { }

		public virtual void Draw()
		{
			switch (this.Shape)
			{
				case OBJECT_SHAPE.CIRC:
					Renderer.Draw2DCircle(
                        this.Transform.Position,
                        20.0f * this.Transform.Scale.Y,
                        this.Color
                    );
                    break;
				case OBJECT_SHAPE.RECT:
					Renderer.Draw2DRect(
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
                    Renderer.Draw2DRect(
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
	}
}

