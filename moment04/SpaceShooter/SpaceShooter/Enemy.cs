using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter
{
    abstract class Enemy : PhysicalObject
    {
        //Konstruktor för att skapa objektet
        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y, speedX, speedY)
        {
        }

        //Uppdaterar fiendens position

        public abstract void Update(GameWindow window);

    }

    //Mina som rör sig fram och tillbaka över skärmen
    class Mine : Enemy
    {
        //Konstruktor
        public Mine(Texture2D texture, float X, float Y)
            : base(texture, X, Y, 6f, 0.3f)
        {
        }

        public override void Update(GameWindow window)
        {
            //Flyttar på fienden
            vector.X += speed.X;

            //Kontrollerar så att den inte åker utanför fönstret på sidorna
            if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
                speed.X *= -1; //Byter riktning på fienden
            vector.Y += speed.Y;

            //Gör fienden inaktiv om den åker ut där nere
            if (vector.Y > window.ClientBounds.Height)
                isAlive = false;
        }
    }

    //Fiende som åker rakt fram
    class Tripod : Enemy
    {
        //Konstruktor
        public Tripod(Texture2D texture, float X, float Y)
            : base(texture, X, Y, 0F, 3F)
        {

        }

        public override void Update(GameWindow window)
        {
            vector.Y += speed.Y;
            if (vector.Y > window.ClientBounds.Height)
                isAlive = false;
        }
    }
}
