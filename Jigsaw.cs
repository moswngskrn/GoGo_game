using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class Jigsaw:Input
    {
        Texture2D j1, j2, j3, j4;
        public bool havej1, havej2, havej3, havej4;

        public List<int> X;
        public List<Texture2D> item;

        public void LoadContent(ContentManager Content)
        {
            j1 = Content.Load<Texture2D>("j1");
            j2 = Content.Load<Texture2D>("j2");
            j3 = Content.Load<Texture2D>("j3");
            j4 = Content.Load<Texture2D>("j4");
            X = new List<int>();
            X.Add(2000);
            X.Add(3000);
            X.Add(4000);
            X.Add(5000);
            item = new List<Texture2D>();
            item.Add(j1);
            item.Add(j2);
            item.Add(j3);
            item.Add(j4);
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (havej1)
            {
                spriteBatch.Draw(j1, new Rectangle(50, 50, 100, 100), Color.White);
            }
            if (havej2)
            {
                spriteBatch.Draw(j2, new Rectangle(121, 48, 100, 100), Color.White);
            }
            if (havej3)
            {
                spriteBatch.Draw(j3, new Rectangle(51, 122, 100, 100), Color.White);
            }
            if (havej4)
            {
                spriteBatch.Draw(j4, new Rectangle(115, 111, 100, 100), Color.White);
            }
        }
    }
}
