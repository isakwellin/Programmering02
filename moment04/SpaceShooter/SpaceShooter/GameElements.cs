using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpaceShooter
{
    static class GameElements
    {
        //Initierar variablar
        static Background background;
        static Menu menu;
        static SpriteFont arial;
        static SpriteBatch spriteBatch;
        static Player player;
        static List<Enemy> enemies;
        static List<GoldCoin> goldCoins;
        static Texture2D goldCoinSprite;

        //Olika gamestates
        public enum State { Menu, Run, Highscore, Quit};
        public static State currentState;

        //Initierar objekt
        public static void Initialize()
        {
            goldCoins = new List<GoldCoin>();
        }

        //Laddar in object/filer
        public static void LoadContent(ContentManager content, GameWindow window)
        {
            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("sprites/menu/start"), (int)State.Run);
            menu.AddItem(content.Load<Texture2D>("sprites/menu/highscore"), (int)State.Highscore);
            menu.AddItem(content.Load<Texture2D>("sprites/menu/exit"), (int)State.Quit);

            background = new Background(content.Load<Texture2D>("sprites/background"), window);

            player = new Player(content.Load<Texture2D>("sprites/ship.png"), 380, 400, 2.5f, 4.5f, content.Load<Texture2D>("sprites/bullet.png"));

            arial = content.Load<SpriteFont>("Fonts/Arial");

            goldCoinSprite = content.Load<Texture2D>("sprites/coin.png");


            //Skapar en mängd fiender med hjälp av metod
            GenerateEnemies(content, window);
        }

        //Uppdaterar menyn
        public static State MenuUpdate(GameTime gameTime)
        {
            return (State)menu.Update(gameTime);
        }

        //Ritar menyn
        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            menu.Draw(spriteBatch);
        }


        //Update-metod för spelet
        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {
            background.Update(window);
            player.Update(window, gameTime);

            //Guldmynten ska uppstå slumpmässigt med en chans på 200
            Random random = new Random();
            int newCoin = random.Next(1, 200);
            if (newCoin == 1)
            {
                //Vart ska guldmyntet uppstå
                int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - goldCoinSprite.Height);

                //Lägger till guldmyntet i listan
                goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }

            foreach (GoldCoin gc in goldCoins.ToList())
            {
                //Kollar om guldmyntet har blivit för gammalt
                if (gc.IsAlive)
                {
                    gc.Update(gameTime);

                    //Kontrollerar kollision med spelaren
                    if (gc.CheckCollision(player))
                    {
                        //Ta bort myntet vid kollision
                        goldCoins.Remove(gc);
                        player.Points++; //Ge spelaren poäng
                    }
                }
                else
                {
                    goldCoins.Remove(gc); //Annars ta bort guldmyntet
                }
            }

            //Går igenom varje fiende, kontrollerar att de lever och om de gör det så uppdatera enemy'n, annars ta bort den

            foreach (Enemy e in enemies.ToList())
            {
                foreach (Player.Bullet b in player.Bullets)
                {
                    if (e.CheckCollision(b))
                    {
                        e.IsAlive = false;
                        player.Points++; //Om fienden blir skjuten, ge spelaren poäng
                    }
                }
                if (e.IsAlive)
                {
                    if (e.CheckCollision(player))
                        player.IsAlive = false;
                    e.Update(window);
                }
                else
                {
                    enemies.Remove(e);
                }
            }

            if (!player.IsAlive) //Spelaren är död
            {
                Reset(content, window); //Återställ spelet
                return State.Menu; //Återgå till menyn
            } 

            return State.Run; //Stanna kvar i Run
        }

        //Metod för att rita ut spelet
        public static void RunDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);

            //Ritar ut fiender
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);

            //Ritar ut guldmynt
            foreach (GoldCoin gc in goldCoins)
                gc.Draw(spriteBatch);


            //Skriver ut antal poäng
            spriteBatch.DrawString(arial, ("points:" + player.Points), new Vector2(0, 0), Color.White);
        }

        //Update metod för highscore listan
        public static State HighScoreUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //Återgå till menyn om man trycker ESC knappen
            if (keyboardState.IsKeyDown(Keys.Escape))
                return State.Menu;
            return State.Highscore; //Stanna kvar i HighScore
        }

        //Ritar ut highscorelistan
        public static void HighScoreDraw (SpriteBatch spriteBatch)
        {
            
        }

        //Återställer alla objekt så att man kan starta ett nytt spel
        private static void Reset(ContentManager content, GameWindow window)
        {
            player.Reset(380, 400, 2.5f, 4.5f);

            //Rensa fienderna sedan skapa nya
            enemies.Clear();
            GenerateEnemies(content, window);

        }

        //Metod som skapar fienderna i början av spelet
        private static void GenerateEnemies(ContentManager content, GameWindow window)
        {
            enemies = new List<Enemy>();
            Random random = new Random();
            Texture2D tmpSprite = content.Load<Texture2D>("sprites/mine.png");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);

                Mine temp = new Mine(tmpSprite, rndX, rndY);

                //Lägger till fienden i listan, totalt skapas 5 minor
                enemies.Add(temp);
            }
            tmpSprite = content.Load<Texture2D>("sprites/tripod.png");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);

                Tripod temp = new Tripod(tmpSprite, rndX, rndY);

                //Lägger till fienden i listan, totalt skapas 5 tripods
                enemies.Add(temp);
            }
        }
    }
}
