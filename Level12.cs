using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GoGo
{
    class Level12
    {
        int displayWidth, displayHeight;
        ContentManager Content;
        Texture2D background;
        Vector2 Position;
        public Runner runner;
        Random ramdom;

        List<Enermy> Enermys;
        List<string> nameEnemy;
        float elapsed, interval;
        public bool Active;
        int numEnermy = 12;

        public Level12(int displayWidth, int displayHeight)
        {
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
            runner = new Runner(displayWidth, displayHeight);
            Enermys = new List<Enermy>();
            ramdom = new Random();
            nameEnemy = new List<string>() { "enamy1_left", "enamy2_left" };
            elapsed = 2;
            interval = 2;
            Active = true;
        }
        public void LoadContent(ContentManager Content)
        {
            this.Content = Content;
            background = Content.Load<Texture2D>("IMG_8302");
            runner.LoadContent(Content);
        }

        void AddEnemy()
        {
            Enermy e = new Enermy(displayWidth, displayHeight);
            e.LoadContent(Content, nameEnemy[ramdom.Next(0, nameEnemy.Count)]);
            e.Position.X = ramdom.Next(displayWidth - 200, displayWidth - 50);
            Enermys.Add(e);
        }

        void CheckTouch()
        {
            Rectangle Rect1, Rect2;
            Rect1 = new Rectangle((int)runner.Position.X - 30, (int)runner.Position.Y - 40, 60, 80);
            for (int i = 0; i < Enermys.Count; i++)
            {
                Rect2 = new Rectangle((int)Enermys[i].Position.X - 30, (int)Enermys[i].Position.X - 40, 60, 80);
                if (Rect1.Intersects(Rect2))
                {
                    if (runner.GetAction == "Attack")
                    {
                        Enermys[i].Health -= 50;
                        if (Enermys[i].Health <= 0)
                        {
                            Enermys[i].Active = false;
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (numEnermy <= 0)
            {
                if (Enermys.Count <= 0)
                {
                    Active = false;
                }
            }
            if (numEnermy > 0)
            {
                elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsed >= interval)
                {
                    AddEnemy();
                    elapsed = 0;
                    numEnermy--;
                }
            }

            runner.Update(gameTime);
            for (int i = 0; i < Enermys.Count; i++)
            {
                Enermys[i].Update(gameTime);
            }

            CheckTouch();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, displayWidth, displayHeight), Color.White);
            runner.Draw(spriteBatch);
            for (int i = Enermys.Count - 1; i >= 0; i--)
            {
                Enermys[i].Draw(spriteBatch);
                if (!Enermys[i].Active)
                {
                    Enermys.RemoveAt(i);
                }
            }
        }
    }
}
