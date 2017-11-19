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
    class Monter
    {
        ContentManager Content;

        Animation monter;
        Vector2 Position;
        public bool Active;
        int destand;
        int VX;
        string RunnerAction;

        public List<BulletMonter> Bullet;
        float elapsed, interval;

        public void LoadContent(ContentManager Content, Vector2 Position)
        {
            this.Content = Content;
            monter = new Animation(Content.Load<Texture2D>("tonberry"), 80, 100, 4, 4, 1, 2, 150, true);
            this.Position = Position;
            Active = true;
            destand = 200;
            VX = 2;
            elapsed = 0;
            interval = 5;

            Bullet = new List<BulletMonter>();
        }

        void AddBullet()
        {
            BulletMonter b = new BulletMonter();
            b.LoadContent(Content, Position, VX*2);
            Bullet.Add(b);
        }

        public void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsed >= interval)
            {
                AddBullet();
                elapsed = 0;
            }


            if (destand <= 0)
            {
                VX = -VX;
                destand = 200;
                if (VX < 0)
                {
                    monter.frameHorizontal = 3;
                }
                else
                {
                    monter.frameHorizontal = 2;
                }
            }


            for(int i = 0; i < Bullet.Count; i++)
            {
                Bullet[i].Update(gameTime);
            }


            destand -= Math.Abs(VX);
            Position.X -= VX;
            Move();
            monter.Update(gameTime, Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = Bullet.Count - 1; i >= 0; i--)
            {
                Bullet[i].Draw(spriteBatch);
                if (!Bullet[i].Active)
                {
                    Bullet.RemoveAt(i);
                }
            }
            monter.Draw(spriteBatch);
        }

        void Move()
        {
            if (RunnerAction == "Right")
            {
                Position.X -= 2;
            }
        }

        public string SetRunerAction
        {
            set
            {
                RunnerAction = value;
            }
        }
        public Vector2 GetPosition
        {
            get
            {
                return Position;
            }
        }
    }
}