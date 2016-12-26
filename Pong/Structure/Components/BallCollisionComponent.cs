using Pong.Structure.Messaging;

namespace Pong.Structure.Components
{
    public class BallCollisionComponent : Component
    {
        public override void Update()
        {
            if (!Entity.HasComponent<VelocityComponent>() || !Entity.HasComponent<SpriteComponent>()) return;

            var velocity = Entity.GetComponent<VelocityComponent>();
            var sprite = Entity.GetComponent<SpriteComponent>();

            #region Walls
            // Bounce off top or bottom
            var walls = Blackboard.GetEntities("wall");

            foreach (var wall in walls)
            {
                var wallSprite = wall.GetComponent<SpriteComponent>();

                if (sprite.BoundingBox.Intersects(wallSprite.BoundingBox))
                {
                    velocity.Y *= -1;
                }
            }

            // Hit left wall - give point to computer 
            if (sprite.X <= 0)
            {
                var message = new Message("point_scored");
                message.Arguments.Add("scorer", "computer");
                Blackboard.PostMessage(message);
            }

            // Hit right wall - give point to player
            if ( sprite.Position.X >= Blackboard.WindowWidth - sprite.Width)
            {
                var message = new Message("point_scored");
                message.Arguments.Add("scorer", "player");
                Blackboard.PostMessage(message);
            }
            #endregion

            #region Paddles

            var paddles = Blackboard.GetEntities("paddle");

            foreach (var paddle in paddles)
            {
                var paddleSprite = paddle.GetComponent<SpriteComponent>();

                if (sprite.BoundingBox.Intersects(paddleSprite.BoundingBox))
                {
                    velocity.X *= -1;
                }
            }

            #endregion
        }
    }
}