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
    class MassageBox:Input
    {
        Texture2D background;
        string Massege;
        SpriteFont spriteFont;
        public bool Active;
        float elapsed, interval;

        public void LoadContent(ContentManager Content,string Massege)
        {
            background = Content.Load<Texture2D>("bg_massage");
            spriteFont = Content.Load<SpriteFont>("Massege");
            this.Massege = Massege;
            Active = false;
            elapsed = 0;
            interval = 5;
        }

        public void Update(GameTime gameTime)
        {
            UpdateInput();
            if (Active)
            {
                elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsed >= interval)
                {
                    Active = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(background, new Rectangle(100,100, 600,200), Color.White);
            spriteBatch.DrawString(spriteFont, Massege, new Vector2(150, 120), Color.White);
        }
    }
}
