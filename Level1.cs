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
    class Level1
    {
        int displayWidth, displayHeight;
        ContentManager Content;
        Texture2D background;
        Vector2 Position;
        public Runner runner;
        Random ramdom;

        public int score;

        List<Enermy> Enermys;
        List<string> nameEnemy;
        float elapsed, interval;
        public bool Active;
        int numEnermy = 6;

        public Level1(int displayWidth, int displayHeight,int numOfEnemy)
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
            numEnermy = numOfEnemy;
            runner.power = 100000;
        }

        public void LoadContent(ContentManager Content,string nameBG)
        {
            this.Content = Content;
            background = Content.Load<Texture2D>(nameBG);
            runner.LoadContent(Content);
        }

        void AddEnemy()
        {
            Enermy e = new Enermy(displayWidth, displayHeight);
            e.LoadContent(Content, nameEnemy[ramdom.Next(0, nameEnemy.Count)]);
            e.Position.X = ramdom.Next(displayWidth - 200, displayWidth - 50);
            Enermys.Add(e);
        }

        void CheckBulletTouchRunner()
        {
            Rectangle Rect1, Rect2;
            Rect1 = new Rectangle((int)runner.Position.X - 30, (int)runner.Position.Y - 40, 60, 80);
            for (int i = 0; i < Enermys.Count; i++)
            {
                for (int j = 0; j < Enermys[i].Bullet.Count; j++)
                {
                    Rect2 = new Rectangle((int)Enermys[i].Bullet[j].Position.X - 15, (int)Enermys[i].Bullet[j].Position.Y - 15, 30, 30);
                    if (Rect1.Intersects(Rect2))
                    {
                        Enermys[i].Bullet[j].Active = false;
                        score -= 50;
                    }
                }
            }
        }

        void CheckKunaiTouchMonter()
        {
            Rectangle Rect1, Rect2;
            for (int i = 0; i < runner.Kunais.Count; i++)
            {
                Rect1 = new Rectangle((int)runner.Kunais[i].Position.X - 25, (int)runner.Kunais[i].Position.Y - 25, 50, 50);
                for (int j = 0; j < Enermys.Count; j++)
                {
                    Rect2 = new Rectangle((int)Enermys[j].Position.X - 30, (int)Enermys[j].Position.Y - 40, 60, 80);
                    if (Rect1.Intersects(Rect2))
                    {
                        Enermys[j].Health -= 200;
                        runner.Kunais[i].Active = false;
                        runner.power-= 100;
                    }
                    if(Enermys[j].Health <= 0)
                    {
                        Enermys[j].Active = false;
                    }
                }
            }
        }
        void CheckTouch()
        {
            Rectangle Rect1, Rect2;
            Rect1 = new Rectangle((int)runner.Position.X - 30, (int)runner.Position.Y - 40, 60, 80);
            for(int i = 0; i < Enermys.Count; i++)
            {
                Rect2 = new Rectangle((int)Enermys[i].Position.X - 30, (int)Enermys[i].Position.X - 40, 60, 80);
                if (Rect1.Intersects(Rect2))
                {
                    if (runner.GetAction == "Attack")
                    {
                        Enermys[i].Health -= 10;
                        if (Enermys[i].Health <= 0)
                        {
                            Enermys[i].Health -= 50;
                        }
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
            for(int i = 0; i < Enermys.Count; i++)
            {
                Enermys[i].Update(gameTime);
            }
            CheckKunaiTouchMonter();
            CheckTouch();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, displayWidth, displayHeight),Color.White);
            runner.Draw(spriteBatch);
            for(int i = Enermys.Count - 1; i>= 0; i--)
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
