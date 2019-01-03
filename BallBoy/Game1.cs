using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BallBoy
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ball; // The Ball texture itself
        Vector2 ballPosition;
        float ballSpeed;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2); // Init ball to center of screen
            ballSpeed = 600;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball = Content.Load<Texture2D>("ball");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                // Update time is not fixed, so to get smooth movement we multiply the speed by the time since last update call
                ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Keep ball within window bounds
            ballPosition.X = MathHelper.Min(MathHelper.Max(ball.Width / 2, ballPosition.X), graphics.PreferredBackBufferWidth - ball.Width / 2);
            ballPosition.Y = MathHelper.Min(MathHelper.Max(ball.Height / 2, ballPosition.Y), graphics.PreferredBackBufferHeight - ball.Height / 2);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw // Draw ball at the var ball position
            (
                ball, // Texture in use 
                ballPosition, // Position
                null, // This, too, is important (???) 
                Color.White,
                0f,
                new Vector2(ball.Width / 2, ball.Height /2), // Set the origin to the center of the ball, not the top left corner
                Vector2.One,
                SpriteEffects.None,
                0f
            ); 
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
