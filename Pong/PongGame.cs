using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Structure;
using Pong.Structure.Components;
using Pong.Structure.Messaging;

namespace Pong
{
    public class PongGame : Game
    {
        private SpriteBatch _spriteBatch;

        private Random _random;

        public PongGame()
        {
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 680;
            Blackboard.WindowHeight = 680;
            graphics.PreferredBackBufferWidth = 960;
            Blackboard.WindowWidth = 960;

            _random = new Random();
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            Blackboard.P1Score = 0;
            Blackboard.P2Score = 0;

            var onPointScoredListener = new MessageListener
            {
                Message = new Message("point_scored"),
                OnMessageRecieved = delegate(Dictionary<string, string> args)
                {
                    var scorer = args["scorer"];

                    if (scorer == null) return;

                    switch (scorer)
                    {
                        case "p1":
                            Blackboard.P1Score++;
                            break;
                        case "p2":
                            Blackboard.P2Score++;
                            break;
                        default:
                            throw new ArgumentException();
                    }

                    // Update text
                    var scoreTextComponent = Blackboard.GetEntity("text_score").GetComponent<TextSpriteComponent>();
                    scoreTextComponent.Text = $"{Blackboard.P1Score} | {Blackboard.P2Score}";
                    scoreTextComponent.Position = Blackboard.Center -
                                                  new Vector2(
                                                      scoreTextComponent.Font.MeasureString(scoreTextComponent.Text).X /
                                                      2f, 180);

                    // Reset ball
                    var ball = Blackboard.GetEntity("ball");
                    ball.GetComponent<VelocityComponent>().Velocity = new Vector2(
                        10 * (_random.Next(0, 2) * 2 - 1),
                        10 * (_random.Next(0, 2) * 2 - 1));
                    ball.GetComponent<SpriteComponent>().Position = Blackboard.Center - new Vector2(16, 16);
                }
            };

            Blackboard.AddMessageListener(onPointScoredListener);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Ball
            var ball = new Entity("ball");
            ball.AddComponent(new SpriteComponent(_spriteBatch)
            {
                Texture = Content.Load<Texture2D>("ball"),
                Position = Blackboard.Center - new Vector2(16, 16)
            });

            ball.AddComponent(new VelocityComponent
            {
                Velocity = new Vector2(
                        10 * (_random.Next(0, 2) * 2 - 1),
                        10 * (_random.Next(0, 2) * 2 - 1))
            });

            ball.AddComponent(new BallCollisionComponent());

            Blackboard.Entities.Add(ball);
            #endregion

            #region Walls
            var wallCount = 1;
            for (var i = 0; i <= Blackboard.WindowHeight - 16; i += Blackboard.WindowHeight - 16)
            {
                var wall = new Entity("wall_" + wallCount);

                wall.AddComponent(new SpriteComponent(_spriteBatch)
                {
                    Texture = Content.Load<Texture2D>("wall"),
                    Position = new Vector2(0, i)
                });

                Blackboard.Entities.Add(wall);
                wallCount++;
            }

            #endregion

            #region Paddles
            var p1Paddle = new Entity("paddle_p1");

            p1Paddle.AddComponent(new SpriteComponent(_spriteBatch)
            {
                Texture = Content.Load<Texture2D>("paddle"),
                Position = new Vector2(16, Blackboard.WindowHeight / 2f - 80)
            });

            p1Paddle.AddComponent(new InputControlComponent(Keys.W, Keys.S));

            Blackboard.Entities.Add(p1Paddle);

            var p2Paddle = new Entity("paddle_p2");

            p2Paddle.AddComponent(new SpriteComponent(_spriteBatch)
            {
                Texture = Content.Load<Texture2D>("paddle"),
                Position = new Vector2(Blackboard.WindowWidth - 32, Blackboard.WindowHeight / 2f - 80)
            });

            p2Paddle.AddComponent(new InputControlComponent(Keys.Up, Keys.Down));

            Blackboard.Entities.Add(p2Paddle);
            #endregion

            #region Text
            var scoreText = new Entity("text_score");
            var scoreFont = Content.Load<SpriteFont>("score");

            scoreText.AddComponent(new TextSpriteComponent(_spriteBatch)
            {
                Font = scoreFont,
                Position = Blackboard.Center - new Vector2(scoreFont.MeasureString("0 | 0").X / 2f, 360 ),
                Text = "0 | 0"
            });

            Blackboard.Entities.Add(scoreText);
            #endregion
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var entity in Blackboard.Entities)
            {
                foreach (var component in entity.Components)
                {
                    if (component.ComponentType == Component.Type.Update) component.Update();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (var entity in Blackboard.Entities)
            {
                foreach (var component in entity.Components)
                {
                    if (component.ComponentType == Component.Type.Draw) component.Update();
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
