using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Senses.Actors;
using Senses.States;
using Senses.Components;

namespace Senses
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SenseTest : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState mouseState;
        KeyboardState keyboardState;

        FoodHunter foodHunter;
        Enemy enemyHunter;
        InputHandler inputHandler;
        StateManager stateManager;
        BackgroundBuilder bgBuilder;

        public SenseTest()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
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
            // TODO: Add your initialization logic here

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

            foodHunter = new FoodHunter(Content.Load<Texture2D>("Char24"), Content.Load<Texture2D>("Checkers_Red"));
            enemyHunter = new Enemy(Content.Load<Texture2D>("Char28"), Content.Load<Texture2D>("Char10"));
            bgBuilder = new BackgroundBuilder(Content.Load<Texture2D>("background"), Content.Load<Texture2D>("desertBg"));

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            inputHandler = new InputHandler(mouseState, keyboardState);

            stateManager = new StateManager(foodHunter, enemyHunter);
            // TODO: use this.Content to load your game content here
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //     Exit();

            // TODO: Add your update logic here
            stateManager.Update(gameTime, inputHandler);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightYellow);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            bgBuilder.Draw(spriteBatch);
            stateManager.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
