using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace HighScoreExample

{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont myFont;
        HighScore highscore;

        enum State { PrintHighScore, EnterHighScore };
        State currentState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            highscore = new HighScore(10);

            if (File.Exists("highscore.txt"))

            {
                highscore.LoadFromFile("highscore.txt");
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            myFont = Content.Load<SpriteFont>("Arial16");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            switch (currentState)
            {
                case State.EnterHighScore: // Skriv in oss i listan
                                           // Fortsätt så länge HighScore.EnterUpdate() returnerar true:
                    if (highscore.EnterUpdate(gameTime, 10))
                        currentState = State.PrintHighScore;
                    break;
                default: // Highscore-listan (tar emot en tangent)
                    KeyboardState keyboardState = Keyboard.GetState();
                    if (keyboardState.IsKeyDown(Keys.E))
                        currentState = State.EnterHighScore;
                    break;
            }


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            _spriteBatch.Begin();
            switch (currentState)
            {
                case State.EnterHighScore: // Skriv in oss i listan
                    highscore.EnterDraw(_spriteBatch, myFont);
                    break;
                default: // Rita ut highscore-listan
                    highscore.PrintDraw(_spriteBatch, myFont);
                    break;
            }
            _spriteBatch.End();
        }

        protected override void UnloadContent()
        {
            highscore.SaveToFile("highscore.txt");
            base.UnloadContent();
        }
    }
}
