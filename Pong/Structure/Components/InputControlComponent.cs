using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong.Structure.Components
{
    public class InputControlComponent : Component
    {
        public override void Update()
        {
            if (!Entity.HasComponent<SpriteComponent>()) return;

            var keyState = Keyboard.GetState();

            var sprite = Entity.GetComponent<SpriteComponent>();

            if (keyState.IsKeyDown(Keys.Up))
            {
                sprite.Y -= 8f;
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                sprite.Y += 8f;
            }

            if (sprite.Y < 16) sprite.Y = 16;
            if (sprite.Y > Blackboard.WindowHeight - sprite.Height - 16)
                sprite.Y = Blackboard.WindowHeight - sprite.Height - 16;
        }
    }
}