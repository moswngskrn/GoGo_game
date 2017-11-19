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
    class Box
    {
        Texture2D box;
        public Vector2 Position;
        float vy, u, g, timer;
        int displayWidth, displayHeight;

        public Box(int displayWidth, int displayHeight)
        {
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
        }

        public void LoadContent(ContentManager Content)
        {
            Position = new Vector2(displayWidth - 60, -100);
            box = Content.Load<Texture2D>("box");
            u = -10;
            g = 20;
            timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (Position.Y < displayHeight - 70)
            {
                vy = u + g * timer;
                Position.Y += vy;
            }
            else
            {
                timer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(box, new Rectangle((int)Position.X, (int)Position.Y, 100, 100), Color.White);
        }
    }
}
