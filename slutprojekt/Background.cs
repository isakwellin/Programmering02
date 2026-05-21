using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpaceShooter
{
    class BackgroundSprite : GameObject
    {
        //Konstruktor
        public BackgroundSprite(Texture2D texture, float X, float Y)
            : base(texture, X, Y)
        {

        }

        //Ändrar positionen för BackgroundSprite objektet
        public void Update(GameWindow window, int nrBackgroundsY)
        {
            vector.Y += 2f; //Flytta bakgrunden
            //Kontrollera om bakgrunden har åkt ut i nedkant
            if (vector.Y > window.ClientBounds.Height)
            {
                //Flytta bilden så att den hamnar ovanför alla andra bilder
                vector.Y = vector.Y - nrBackgroundsY * texture.Height;
            }
        }
    }

    //Ritar ut en 2d-vektor med bakgrundsbilder
    class Background
    {
        BackgroundSprite[,] background;
        int nrBackgroundsX, nrBackgroundsY;

        //Konstruktor som skapar alla BackgroundSprite-objekt i en 2d-vektor
        public Background (Texture2D texture, GameWindow window)
        {
            //Hur många bilder ska vi ha på bredden?
            double tmpX = (double)window.ClientBounds.Width / texture.Width;

            //Avrunda uppåt med Math.Ceiling()
            nrBackgroundsX = (int)Math.Ceiling(tmpX);

            //Hur många bilder ska vi ha på höjden?
            double tmpY = (double)window.ClientBounds.Height / texture.Height;

            //Avrunda uppåt med Math.Ceiling(), lägg till 1 extra
            nrBackgroundsY = (int)Math.Ceiling(tmpX) + 1;

            //Sätt storlek på vektorn
            background = new BackgroundSprite[nrBackgroundsX, nrBackgroundsY];

            //Fyll på vektorn med BackgroundSprite-objekt
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    int posX = i * texture.Width;
                    //Gör att den först hamnar ovanför skärmen
                    int posY = j * texture.Height - texture.Height;
                    background[i, j] = new BackgroundSprite(texture, posX, posY);
                }
            }


        }

        //Uppdaterar positionen för samtliga BackgroundSprite-objekt
        public void Update(GameWindow window)
        {
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    background[i, j].Update(window, nrBackgroundsY);
                }
            }
        }

        //Ritar ut samtliga BackgroundSprite-objekt
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    background[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
