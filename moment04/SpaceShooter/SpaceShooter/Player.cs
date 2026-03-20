using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpaceShooter
{
    class Player : PhysicalObject
    {
        List<Bullet> bullets; //Alla skott
        Texture2D bulletTexture; //Skottets bild
        double timeSinceLastBullet = 0; // I millisekunder
        int points = 0;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        //Konstruktor för att skapa spelar objektet

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D bulletTexture)
            : base(texture, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
        }

        public void Update(GameWindow window, GameTime gameTime)
        {
            //Läser in tangentbordstryckningar
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                isAlive = false;

            //Kollar efter nedtrycka tangenter för att styra skeppet OM det inte är på väg ut ur fönstret
            if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.D))
                    vector.X += speed.X;
                if (keyboardState.IsKeyDown(Keys.A))
                    vector.X -= speed.X;
            }

            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.S))
                    vector.Y += speed.Y;
                if (keyboardState.IsKeyDown(Keys.W))
                    vector.Y -= speed.Y;
            }

            //Kollar så att skeppet inte har åkt från kanten, om det har det så återställ dess position

            //Har det åkt ut vänster
            if (vector.X < 0)
                vector.X = 0;
            //Höger
            if (vector.X > window.ClientBounds.Width - texture.Width)
            {
                vector.X = window.ClientBounds.Width - texture.Width;
            }
            //Upp
            if (vector.Y < 0)
                vector.Y = 0;
            //Ner
            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
                vector.Y = window.ClientBounds.Height - texture.Height;
            }

            //Spelaren skjuter
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                //Kontrollera att spelaren får skjuta
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 200)
                {
                    //Skapar skottet
                    Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width / 2, vector.Y);

                    //Lägger till det i listan
                    bullets.Add(temp);

                    //Sätter timeSinceLastBullet till detta ögonblick
                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            foreach (Bullet b in bullets.ToList())
            {
                //Flyttar på skottet
                b.Update();
                //Kontrollerar att skottet inte är "dött"
                if (!b.IsAlive)
                    bullets.Remove(b);
            }
        }

        //Ritar ut bilden på skärmen
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            foreach (Bullet b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }

        public List<Bullet> Bullets { get { return bullets; } }

        //Bullet, en klass för att skapa skott
        public class Bullet : PhysicalObject
        {
            private List<Bullet> bullets;

            public Bullet(Texture2D texture, float X, float Y)
                : base(texture, X, Y, 0, 3f)
            {

            }

            //Uppdaterar skottets position
            public void Update()
            {
                vector.Y -= speed.Y;
                if (vector.Y < 0)
                    isAlive = false;
            }
        }

        //Reset(), återställer spelaren för ett nytt spel
        public void Reset(float X, float Y, float speedX, float speedY)
        {
            //Återställ position och hastighet
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;

            //Återställ alla skott
            bullets.Clear();
            timeSinceLastBullet = 0;

            //Återställ poäng
            points = 0;

            //Gör så att spelaren lever igen
            isAlive = true;
        }
    }
}

