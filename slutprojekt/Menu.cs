using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Menu
    {
        List<MenuItem> menu; //Lista på menuItems
        int selected = 0; //Första valet i listan är valt
        float currentHeight = 0; //Används för att rita ut menuItems på olika höjd
        double lastChange = 0; //För att pausa tangenttryckningar så att det inte går för fort när man bläddrar
        int defaultMenuState; //Det state som representerar själva menyn

        //Konstruktor som skapar listan med menuItems
        public Menu(int defaultMenuState)
        {
            menu = new List<MenuItem>();
            this.defaultMenuState = defaultMenuState;
        }

        //AddItem(), lägger till ett menyval i listan
        public void AddItem(Texture2D itemTexture, int state)
        {
            //Sätt höjd på item
            float X = 0;
            float Y = 0 + currentHeight;

            //Ändra höjd efter items höjd + 20 pixlar för lite extra mellanrum
            currentHeight += itemTexture.Height + 20;

            //Skapa ett temporärt objekt och lägg det i listan
            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add(temp);
        }

        //Update(), kollar om användaren tryckt på någon tangent
        public int Update(GameTime gameTime)
        {
            //Läser av tangenttryckningar
            KeyboardState keyboardState = Keyboard.GetState();

            //Byte mellan olika menyval med pauser så att det inte ändras för snabbt
            if (lastChange + 130 < gameTime.TotalGameTime.TotalMilliseconds)
            {
                //Gå ett steg ned i menyn
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    selected++;
                    //Om vi gått utanför möjliga valen ska det gå tillbaka till första menyvalet
                    if (selected > menu.Count - 1)
                        selected = 0; //Första meny alet
                }
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    selected--;
                    //Om vi gått utanför åt andra hållet ska sista menyvalet väljas
                    if (selected < 0)
                        selected = menu.Count - 1; //Sista menyvalet

                }

                //Ställ lastchange till exakt detta ögonblick
                lastChange = gameTime.TotalGameTime.TotalMilliseconds;
            }

            //Välj ett menyval med enter
            if (keyboardState.IsKeyDown(Keys.Enter))
                return menu[selected].State; //Returnera menyvalets state

            //Om inget menyval har valts, så stannar vi kvar i menyn
            return defaultMenuState;
        }

        //Draw(), ritar ut menyn
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < menu.Count; i++)
            {
                //Om vi har ett aktivt meny val ritar vi ut det med en speciell färgtoning
                if(i == selected)
                {
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.RosyBrown);
                }
                else //Annars ingen färgtoning alls
                {
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.White);
                }
            }
        }



        //Container-klass för ett meny val
        class MenuItem
        {
            Texture2D texture; //Bilden för menyvalet
            Vector2 position; //Position för menyvalet
            int currentState; //Menyvalets state

            //Konstruktor

            public MenuItem(Texture2D texture, Vector2 position, int currentState)
            {
                this.texture = texture;
                this.position = position;
                this.currentState = currentState;
            }

            //Get- egenskaper för Menuitem
            public Texture2D Texture { get { return texture; } }
            public Vector2 Position { get { return position; } }
            public int State { get { return currentState; } }
        }
    }
}
