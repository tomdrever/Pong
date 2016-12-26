using Microsoft.Xna.Framework;

namespace Pong.Structure.Components
{
    public class ComputerControlComponent : Component
    {
        public override void Update()
        {
            if (!Entity.HasComponent<SpriteComponent>()) return;

            var sprite = Entity.GetComponent<SpriteComponent>();

            var ballSprite = Blackboard.GetEntity("ball").GetComponent<SpriteComponent>();

            const float speed = 8f;

            // Don't move ALL the time
            if (ballSprite.X > 320)
            {
                // Ball is above paddle
                if (ballSprite.Y <= sprite.Y)
                {
                    sprite.Y -= speed;
                }

                // Ball is below paddle 
                else
                {
                    sprite.Y += speed;
                }
            }

            if (sprite.Y < 16) sprite.Y = 16;
            if (sprite.Y > Blackboard.WindowHeight - sprite.Height - 16)
                sprite.Y = Blackboard.WindowHeight - sprite.Height - 16;
        }
    }
}
