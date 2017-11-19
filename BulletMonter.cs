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
    class BulletMonter
    {
        Animation Bullet;
        public Vector2 Position;
        int VX;
        public bool Active;

        public void LoadContent(ContentManager Content,Vector2 Position,int VX)
        {
            Bullet = new Animation(Content.Load<Texture2D>("darkness_001"), 100,100, 5, 6, 1, 1, 150, true);
            Bullet.Active = true;
            this.Position = Position;
            this.VX = VX;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            Position.X += VX;
            if (Position.X <= -50)
            {
                Active = false;
            }
            Bullet.Update(gameTime, Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Bullet.Draw(spriteBatch);
        }
    }
}
