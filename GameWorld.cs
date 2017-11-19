using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo
{
    class GameWorld:Input
    {
        ContentManager Content;
        float DisplayWidth, DisplayHeight;

        public int score=0;
        public int Power=0;

        Runner runer;
        Butterfly butterfly;

        List<Food> Foods;
        float elapsed, interval;

        List<Monter> Monters;
        float elapsedMon, intervalMon;

        public bool isEndSeen = false;

        Random random;

        Box box;

        //Background
        List<Background> Backgrounds;
        List<string> nameBackground;

        public GameWorld(float displayWidth, float displayHeight)
        {
            DisplayWidth = displayWidth;
            DisplayHeight = displayHeight;
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = Content;

            runer = new Runner(DisplayWidth, DisplayHeight);
            butterfly = new Butterfly();

            runer.LoadContent(Content);
            butterfly.LoadContent(Content);

            Foods = new List<Food>();
            elapsed = 0;
            interval = 100;

            random = new Random();

            //Backgrounds
            Backgrounds = new List<Background>();
            nameBackground = new List<string>() { "Background1", "Background2" , "Background3" , "Background4" , "Background5" , "Background6" };
            AddBackgrounds(0);


            Monters = new List<Monter>();
            elapsedMon = 0;
            intervalMon = 5;

            //Box
            box = new Box((int)DisplayWidth, (int)DisplayHeight);
            box.LoadContent(Content);
        }

        public void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= interval)
            {
                AddFood();
                elapsed = 0;
            }


            UpdateFood(gameTime);

 
            runer.SetEndSeen = isEndSeen;

            elapsedMon += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedMon >= intervalMon)
            {
                if (!isEndSeen)
                {
                    AddMonter();
                }
                elapsedMon = 0;
            }

            if (isEndSeen)
            {
                box.Update(gameTime);
            }

            if (!isEndSeen)
            {
                if (runer.Position.X >= DisplayWidth / 2)
                {
                    UpdateBackgrounds();
                }
            }

            Power = runer.power;
            runer.Update(gameTime);

            UpdateMonter(gameTime);
            butterfly.Update(gameTime);
            CheckBulletTouchRunner();
            CheckButterflyTouchFood();
            CheckKunaiTouchMonter();
        }

        void AddFood()
        {
            Food F = new Food(DisplayWidth, DisplayHeight);
            F.LoadContent(Content, new Vector2(Backgrounds[0].GetPositionX+DisplayWidth + 100, random.Next(50, (int)DisplayHeight - 100)));
            Foods.Add(F);
        }

        void UpdateFood(GameTime gameTime)
        {
            for(int i = 0; i < Foods.Count; i++)
            {
                if (runer.Position.X >= DisplayWidth / 2)
                {
                    Foods[i].SetRunerAction = runer.GetAction;
                }
                
                Foods[i].Update(gameTime);
            }
        }

        void CheckButterflyTouchFood()
        {
            Rectangle ract1, ract2;
            ract1 = new Rectangle((int)butterfly.GetPosition.X - 40, (int)butterfly.GetPosition.Y - 40, 80, 80);
            for(int i = 0; i < Foods.Count; i++)
            {
                ract2 = new Rectangle((int)Foods[i].GetPosition.X - 5, (int)Foods[i].GetPosition.Y - 5, 10, 10);
                if (ract1.Intersects(ract2))
                {
                    Foods[i].Active = false;
                    runer.power += 5;
                }
            }
        }

        void CheckBulletTouchRunner()
        {
            Rectangle Rect1, Rect2;
            Rect1 = new Rectangle((int)runer.Position.X - 30, (int)runer.Position.Y - 40, 60, 80);
            for(int i = 0; i < Monters.Count; i++)
            {
                for(int j = 0; j < Monters[i].Bullet.Count; j++)
                {
                    Rect2 = new Rectangle((int)Monters[i].Bullet[j].Position.X - 15, (int)Monters[i].Bullet[j].Position.Y - 15, 30, 30);
                    if (Rect1.Intersects(Rect2))
                    {
                        Monters[i].Bullet[j].Active = false;
                        score -= 50;
                    }
                }
            }
        }

        void CheckKunaiTouchMonter()
        {
            Rectangle Rect1, Rect2;
            for(int i = 0; i < runer.Kunais.Count; i++)
            {
                Rect1 = new Rectangle((int)runer.Kunais[i].Position.X - 25, (int)runer.Kunais[i].Position.Y - 25, 50, 50);
                for(int j = 0; j < Monters.Count;j++)
                {
                    Rect2 = new Rectangle((int)Monters[j].GetPosition.X - 30, (int)Monters[j].GetPosition.Y - 40, 60, 80);
                    if (Rect1.Intersects(Rect2))
                    {
                        Monters[j].Active = false;
                        runer.Kunais[i].Active = false;
                        score += 150;
                    }
                }
            }
        }

        void AddMonter()
        {
            Monter m = new Monter();
            m.LoadContent(Content, new Vector2(Backgrounds[0].GetPositionX + DisplayWidth + 100, DisplayHeight - 50));
            Monters.Add(m);
        }
        void UpdateMonter(GameTime gameTime)
        {
            for(int i = 0; i < Monters.Count; i++)
            {
                Monters[i].SetRunerAction = runer.GetAction;
                Monters[i].Update(gameTime);
            }
        }

        //Add Backgrounds
        public void AddBackgrounds(int posX)
        {
            Background b = new Background(DisplayWidth, DisplayHeight);
            b.LoadContent(Content, nameBackground[0]);
            b.X = posX;
            nameBackground.RemoveAt(0);
            Backgrounds.Add(b);
        }
        //Check Backgrounds End
        void CheckBackgroundsEnd()
        {
            if (Backgrounds.Count >= 1)
            {
                if (Backgrounds[0].GetWidth - Backgrounds[0].X <= DisplayWidth && Backgrounds.Count == 1)
                {
                    isEndSeen = true;
                    //AddBackgrounds(-(int)DisplayWidth);
                }
                if (Backgrounds[0].X >= Backgrounds[0].GetWidth && nameBackground.Count > 0)
                {
                    Backgrounds.RemoveAt(0);
                }
            }
        }
        //Update Backgrounds
        void UpdateBackgrounds()
        {
            for (int i = 0; i < Backgrounds.Count; i++)
            {
                Backgrounds[i].SetRunerAction = runer.GetAction;
                Backgrounds[i].Update();
            }

            //Background
            CheckBackgroundsEnd();
        }

        public bool CheckTuchBox()
        {
            Rectangle Rect1, Rect2;
            Rect1 = new Rectangle((int)runer.Position.X - 30, (int)runer.Position.Y - 40, 60, 80);
            Rect2 = new Rectangle((int)box.Position.X+50, (int)box.Position.Y, 200, 200);
            if (Rect1.Intersects(Rect2))
            {
                return true;
            }
            return false;
        }

        public float SetPositionXRunner
        {
            set
            {
                runer.Position.X = value;
            }
        }

        public float SetPosYBox
        {
            set
            {
                box.Position.Y = value;
            }
        }

        public Vector2 GetPositionRunner
        {
            get
            {
                return runer.Position;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Background
            for (int i = Backgrounds.Count - 1; i >= 0; i--)
            {
                Backgrounds[i].Draw(spriteBatch);

            }

            runer.Draw(spriteBatch);
            butterfly.Draw(spriteBatch);

            for(int i= Foods.Count - 1; i >= 0; i--)
            {
                Foods[i].Draw(spriteBatch);
                if (!Foods[i].Active)
                {
                    Foods.RemoveAt(i);
                }
            }

            for (int i = Monters.Count - 1;i>= 0;i--){
                Monters[i].Draw(spriteBatch);
                if (!Monters[i].Active)
                {
                    Monters.RemoveAt(i);
                }
            }
            box.Draw(spriteBatch);
            
        }
    }
}
