using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter
{
    class GameObject
    {
        protected Texture2D texture; //Skeppets textur
        protected Vector2 vector; //Skeppets position

        //Konstruktor för att skapa objektet

        public GameObject (Texture2D texture, float X, float Y)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
        }

        //Ritar ut bilden på skärmen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }

        //Egenskaper för GameObject
        public float X { get { return vector.X;  } }
        public float Y { get { return vector.Y; } }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }


    }

    abstract class MovingObject : GameObject
    {
        protected Vector2 speed; //Hastigheten för objektet

        //Konstruktor för att skapa objektet
        public MovingObject(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y) 
        {
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }
    }

    //Klass för objekt som kan kollidera med andra objekt

    abstract class PhysicalObject : MovingObject
    {
        protected bool isAlive = true;

        //Konstruktor för att skapa spelar-objektet

        public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base (texture, X, Y, speedX, speedY)
        {

        }

        //Kontrollerar om objektet har kolliderat med ett annat

        public bool CheckCollision(PhysicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Convert.ToInt32(Width), Convert.ToInt32(Height));
            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));

            return myRect.Intersects(otherRect);
        }

        //Egenskaper för PhysicalObject

        public bool IsAlive 
        
        {
        get { return isAlive; }
        set { isAlive = value; }
        }
    }



}
