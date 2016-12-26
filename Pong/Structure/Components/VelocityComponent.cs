using Microsoft.Xna.Framework;

namespace Pong.Structure.Components
{
    public class VelocityComponent : Component
    {
        public Vector2 Velocity { get; set; }

        public float X
        {
            get { return Velocity.X; }
            set { Velocity = new Vector2(value, Velocity.Y);}
        }

        public float Y
        {
            get { return Velocity.Y; }
            set { Velocity = new Vector2(Velocity.X, value); }
        }

        public VelocityComponent(float x, float y) : base()
        {
            Velocity = new Vector2(x, y);
        }

        public VelocityComponent() : base() {}

        public override void Update()
        {
            if (!Entity.Active) return;
            if (!Entity.HasComponent<SpriteComponent>()) return;

            Entity.GetComponent<SpriteComponent>().Position += Velocity;
        }
    }
}
