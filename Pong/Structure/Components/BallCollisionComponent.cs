using Pong.Structure.Messaging;

namespace Pong.Structure.Components
{
    public class BallCollisionComponent : Component
    {
        public override void Update()
        {
            if (!Entity.Active) return;
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

            // Hit left wall - give point to p2 
            if (sprite.X <= 0)
            {
                var message = new Message("point_scored");
                message.Arguments.Add("scorer", "p2");
                Messageboard.PostMessage(message);
            }

            // Hit right wall - give point to p1
            if ( sprite.Position.X >= Blackboard.WindowWidth - sprite.Width)
            {
                var message = new Message("point_scored");
                message.Arguments.Add("scorer", "p1");
                Messageboard.PostMessage(message);
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