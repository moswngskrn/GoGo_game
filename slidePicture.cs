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
    class slidePicture
    {
        Texture2D Texture;
        Vector2 Position;
        public bool Active = false;

        public void LoadContent(ContentManager Content,string name)
        {
            Texture = Content.Load<Texture2D>(name);
            Position = new Vector2(2000, 100);
        }

        public void Update()
        {
            Position.X -= 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,new Rectangle((int)Position.X, (int)Position.Y, 200, 200),Color.White);
        }
    }
}
