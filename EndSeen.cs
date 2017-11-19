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
    class EndSeen
    {
        SpriteFont Text;
        Texture2D Sow;
        Texture2D bg;
        int VY = 800;

        public void LoadContent(ContentManager Content)
        {
            Text = Content.Load<SpriteFont>("Massege");
            Sow = Content.Load<Texture2D>("s");
            bg = Content.Load<Texture2D>("IMG_8408");
        }

        public void Update()
        {
            VY -= 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, new Rectangle(0, 0, 800, 500), Color.White);
            spriteBatch.Draw(Sow, new Rectangle(300, VY,200,350), Color.White);
            spriteBatch.DrawString(Text, "ขอให้คุณโชคดีในการใช้ชีวิตในมหาวิทยาลัยนเรศวร", new Vector2(100,VY+380), Color.Black);
        }
    }
}
