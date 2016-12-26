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

        public PongGame()
        {
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 680;
            Blackboard.WindowHeight = 680;
            graphics.PreferredBackBufferWidth = 960;
            Blackboard.WindowWidth = 960;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            var onPointScoredListener = new MessageListener
            {
                Message = new Message("point_scored"),
                OnMessageRecieved = delegate(Dictionary<string, string> args)
                {
                    var scorer = args["scorer"];

                    if (scorer == null) return;

                    switch (scorer)
                    {
                        case "player":
                            Console.WriteLine("player");
                            break;
                        case "computer":
                            Console.WriteLine("comp");
                            break;
                        default:
                            throw new ArgumentException();
                    }

                    var ball = Blackboard.GetEntity("ball");
                    ball.GetComponent<VelocityComponent>().Velocity = new Vector2(10f, -10f);
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

            ball.AddComponent(new VelocityComponent(10f, -10f));

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
            var playerPaddle = new Entity("paddle_player");

            playerPaddle.AddComponent(new SpriteComponent(_spriteBatch)
            {
                Texture = Content.Load<Texture2D>("paddle"),
                Position = new Vector2(16, Blackboard.WindowHeight / 2f - 80)
            });

            playerPaddle.AddComponent(new InputControlComponent());

            Blackboard.Entities.Add(playerPaddle);

            var computerPaddle = new Entity("paddle_computer");

            computerPaddle.AddComponent(new SpriteComponent(_spriteBatch)
            {
                Texture = Content.Load<Texture2D>("paddle"),
                Position = new Vector2(Blackboard.WindowWidth - 32, Blackboard.WindowHeight / 2f - 80)
            });

            computerPaddle.AddComponent(new ComputerControlComponent());

            Blackboard.Entities.Add(computerPaddle);
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
