using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpaceShooter
{
    static class GameElements
    {
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

            //Generar fiender under spelets gång
            Random random = new Random();
            int newEnemy = random.Next(1, 180);
            if (newEnemy == 1)
            {
                //Fienden är en mina
                int enemyChoice = random.Next(1, 3);
                if (enemyChoice == 1)
                {
                    Texture2D tmpSprite = content.Load<Texture2D>("sprites/mine.png");
                    int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                    int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);
                    Mine temp = new Mine(tmpSprite, 2, rndX, rndY);
                    enemies.Add(temp);
                }

                //Fienden är en tripod
                else if (enemyChoice == 2)
                {
                    Texture2D tmpSprite = content.Load<Texture2D>("sprites/tripod.png");
                    int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                    int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);
                    Tripod temp = new Tripod(tmpSprite, rndX, rndY);
                    enemies.Add(temp);
                }

                //Fienden är en asteroid
                else if (enemyChoice == 3)
                {
                    Texture2D tmpSprite = content.Load<Texture2D>("sprites/asteroid.png");
                    int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                    int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);
                    Asteroid temp = new Asteroid(tmpSprite, rndX, rndY);
                    enemies.Add(temp);
                }

                //Fienden är en fälla
                else if (enemyChoice == 3)
                {
                    Texture2D tmpSprite = content.Load<Texture2D>("sprites/trap.png");
                    int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                    int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);
                    Trap temp = new Trap(tmpSprite, rndX, rndY);
                    enemies.Add(temp);
                }
            }

            //Guldmynten ska uppstå slumpmässigt med en chans på 200
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
                    if (e.CheckCollision(b)) //Om ett skott träffar en fiende
                    {
                        e.IsAlive = false; //Ta bort fienden
                        b.IsAlive = false; //Ta bort skottet
                        player.Points += e.points; //Om fienden blir skjuten, ge spelaren poäng
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
            player.Reset(); //Ställ om spelarens variablar
            goldCoins.Clear(); //Töm listan med guldmynt
            enemies.Clear(); //Töm listan med fiender
            GenerateEnemies(content, window); //Generera nya fiender

        }

        //Metod som skapar fienderna i början av spelet
        private static void GenerateEnemies(ContentManager content, GameWindow window)
        {
            enemies = new List<Enemy>();
            Random random = new Random();
            Texture2D tmpSprite = content.Load<Texture2D>("sprites/mine.png");
            for (int i = 0; i < 2; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);

                Mine temp = new Mine(tmpSprite, 2, rndX, rndY);

                //Lägger till fienden i listan, totalt skapas 2 minor
                enemies.Add(temp);
            }
            tmpSprite = content.Load<Texture2D>("sprites/tripod.png");
            for (int i = 0; i < 2; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);

                Tripod temp = new Tripod(tmpSprite, rndX, rndY);

                //Lägger till fienden i listan, totalt skapas 2 tripods
                enemies.Add(temp);
            }
            tmpSprite = content.Load<Texture2D>("sprites/asteroid.png");
            for (int i = 0; i < 2; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);

                Asteroid temp = new Asteroid(tmpSprite, rndX, rndY);

                //Lägger till fienden i listan, totalt skapas 2 asteroider
                enemies.Add(temp);
            }
            tmpSprite = content.Load<Texture2D>("sprites/trap.png");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - tmpSprite.Height);

                Trap temp = new Trap(tmpSprite, rndX, rndY);

                //Lägger till fienden i listan, totalt skapas 5 fällor
                enemies.Add(temp);
            }
        }
    }
}
