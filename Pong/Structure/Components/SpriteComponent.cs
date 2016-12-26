using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Structure.Components
{
    public class SpriteComponent : Component
    {
        private readonly SpriteBatch _spriteBatch;

        public Texture2D Texture { get; set; }
        public Vector2 Size => Texture.Bounds.Size.ToVector2();
        public float Width => Texture.Width;
        public float Height => Texture.Height;
        public Vector2 Position { get; set; }

        public float X
        {
            get { return Position.X; }
            set { Position = new Vector2(value, Position.Y);}
        }

        public float Y
        {
            get { return Position.Y; }
            set { Position = new Vector2(Position.X, value); }
        }

        public Rectangle BoundingBox => new Rectangle((int)X, (int)Y, (int)Width, (int)Height);

        public SpriteComponent(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch; 

            ComponentType = Type.Draw;
        }

        public override void Update()
        {
            _spriteBatch.Draw(Texture, Position);
        }
    }
}