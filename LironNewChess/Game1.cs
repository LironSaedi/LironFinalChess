using Chess.IO;
using Chess.Managers;
using Chess.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Chess
{

    public class DriverClass : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random Random = new Random(); 

        ScreenManager screenManager;

        Input currentInput;
        Input previousInput;

        public DriverClass()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Constants.TILESIZE * 8;
            graphics.PreferredBackBufferHeight = Constants.TILESIZE * 8;
            graphics.ApplyChanges();

            Window.Title = "Chess";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            previousInput = new Input();
            currentInput = new Input();

            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            File.WriteAllText("Scores.json", null);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentService.Instance.LoadContent(this.Content, GraphicsDevice, spriteBatch);

            screenManager = new ScreenManager(new GameScreen(), this);
        }

        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            screenManager.ChangeScreen();

            previousInput.Keyboard = currentInput.Keyboard;
            previousInput.Mouse = currentInput.Mouse;

            currentInput.Update();

            screenManager.Update(gameTime, currentInput, previousInput);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            screenManager.Draw(gameTime,spriteBatch);

            base.Draw(gameTime);
        }
    }
}
