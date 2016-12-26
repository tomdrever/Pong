using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Structure.Components
{
    public class TextSpriteComponent : Component
    {
        public string Text { get; set; }

        public SpriteFont Font { get; set; }

        private readonly SpriteBatch _spriteBatch;

        public Vector2 Position { get; set; }

        public float X
        {
            get { return Position.X; }
            set { Position = new Vector2(value, Position.Y); }
        }

        public float Y
        {
            get { return Position.Y; }
            set { Position = new Vector2(Position.X, value); }
        }

        public TextSpriteComponent(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;

            ComponentType = Type.Draw;
        }

        public override void Update()
        {
            if (!Entity.Active) return;
            _spriteBatch.DrawString(Font, Text, Position, Color.White);
        }
    }
}