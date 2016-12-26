using Microsoft.Xna.Framework.Input;

namespace Pong.Structure.Components
{ 
    public class InputControlComponent : Component
    {
        public Keys KeyUp { get; set; }
        public Keys KeyDown { get; set; }

        public InputControlComponent(Keys keyUp, Keys keyDown)
        {
            KeyUp = keyUp;
            KeyDown = keyDown;
        }

        public override void Update()
        {
            if (!Entity.HasComponent<SpriteComponent>()) return;

            var keyState = Keyboard.GetState();

            var sprite = Entity.GetComponent<SpriteComponent>();

            if (keyState.IsKeyDown(KeyUp))
            {
                sprite.Y -= 8f;
            }

            if (keyState.IsKeyDown(KeyDown))
            {
                sprite.Y += 8f;
            }

            if (sprite.Y < 16) sprite.Y = 16;
            if (sprite.Y > Blackboard.WindowHeight - sprite.Height - 16)
                sprite.Y = Blackboard.WindowHeight - sprite.Height - 16;
        }
    }
}