using Chess.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Managers
{
    class ScreenManager : IScreenManager
    {
        #region Fields

        private Stack<IScreen> listOfScreens = new Stack<IScreen>();
        private IScreen currentScreen;

        private DriverClass game;

        #endregion

        #region Constructors

        public ScreenManager(IScreen startScreen, DriverClass game)
        {
            this.game = game;

            currentScreen = startScreen;
            PushScreen(currentScreen);

            currentScreen.Initialize(this);
        }

        #endregion

        #region Methods

        public void PushScreen(IScreen screen)
        {
            listOfScreens.Push(screen);
        }

        public void PopScreen()
        {
            listOfScreens.Pop();
            currentScreen = listOfScreens.Peek();
        }

        public void RemoveAllScreens()
        {
            while (listOfScreens.Count > 0)
            {
                PopScreen();
            }
        }

        public void ChangeScreen()
        {
            if (listOfScreens.Peek() != currentScreen)
            {
                currentScreen = listOfScreens.Peek();

                currentScreen.Initialize(this);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            currentScreen.Draw(gameTime, spriteBatch);
        }

        public void ExitGame()
        {
            game.Exit();
        }

        public void Update(GameTime gameTime, Input curInput, Input prevInput)
        {
            currentScreen.Update(gameTime, curInput, prevInput);
        }

        #endregion
    }
}
