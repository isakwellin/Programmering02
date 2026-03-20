using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GameElements.currentState = GameElements.State.Menu;
            GameElements.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElements.LoadContent(Content, Window);
        }

        //Spel loopen
        protected override void Update(GameTime gameTime)
        {
            //Stänger av spelet om man trycker på Back-knappen med en gamepad
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Hanterar gamestates

            switch (GameElements.currentState)
            {
                case GameElements.State.Run: //Kör spelet
                    GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                    break;

                case GameElements.State.Highscore: //HighScore listan
                    GameElements.currentState = GameElements.HighScoreUpdate();
                    break;

                case GameElements.State.Quit: //Avsluta spelet
                    this.Exit();
                    break;

                default: //Menyn
                    GameElements.currentState = GameElements.MenuUpdate(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Rensar skärmen
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Rita ut saker på skärmen
            spriteBatch.Begin();

            switch (GameElements.currentState)
            {
                case GameElements.State.Run: //Kör spelet
                    GameElements.RunDraw(spriteBatch);
                    break;

                case GameElements.State.Highscore: //HighScore listan
                    GameElements.HighScoreDraw(spriteBatch);
                    break;

                case GameElements.State.Quit: //Avsluta spelet
                    this.Exit();
                    break;

                default: //Menyn
                    GameElements.MenuDraw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
