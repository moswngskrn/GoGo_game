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
    class Enermy
    {
        int displayWidth, displayHeight;
        Animation animation;
        public Vector2 Position;
        float vy, u, g, timer;
        public bool Active;
        HealthBar healthBar;
        public int Health = 1000;

        ContentManager Content;

        public List<BulletMonter> Bullet;
        float elapsed, interval;

        public Enermy(int displayWidth, int displayHeight)
        {
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
            elapsed = 0;
            interval = 5;
            Bullet = new List<BulletMonter>();
        }

        void AddBullet()
        {
            BulletMonter b = new BulletMonter();
            b.LoadContent(Content, Position, -5);
            Bullet.Add(b);
        }

        public void LoadContent(ContentManager Content,string name)
        {
            animation = new Animation(Content.Load<Texture2D>(name), 80,100, 10, 1, 1, 1, 300, true);
            animation.Active = true;
            Position = new Vector2(displayWidth - 60, -50);
            u = -10;
            g = 20;
            timer = 0;
            Active = true;
            healthBar = new HealthBar();
            healthBar.LoadContent(Content);
            this.Content = Content;
        }

        public void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsed >= interval)
            {
                AddBullet();
                elapsed = 0;
            }

            for(int i = 0; i < Bullet.Count; i++)
            {
                Bullet[i].Update(gameTime);
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (Position.Y < displayHeight - 70)
            {
                vy = u + g * timer;
                Position.Y += vy;
            }
            else
            {
                Position.Y = displayHeight - 70;
                Position.X -= 0.3f;
            }
            healthBar.Update(gameTime, new Vector2(Position.X - 40, Position.Y - 50), Health);
            animation.Update(gameTime, Position);
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
            for(int i = Bullet.Count - 1; i >= 0; i--)
            {
                Bullet[i].Draw(spriteBatch);
            }
        }
    }
}
