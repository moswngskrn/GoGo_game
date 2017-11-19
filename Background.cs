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
    class Background:Input
    {
        float DisplayWidth, DisplayHeight;

        Texture2D Texture;
        Vector2 Position;
        Rectangle destinationRectangle;

        int x = 0;

        string RunnerAction;

        public Background(float displayWidth, float displayHeight)
        {
            DisplayWidth = displayWidth;
            DisplayHeight = displayHeight;
        }

        public void LoadContent(ContentManager Content,string fileNameOfPicture)
        {
            Texture = Content.Load<Texture2D>(fileNameOfPicture);
        }
        public void Update()
        {
            UpdateInput();
            Move();
            destinationRectangle = new Rectangle(-x, 0, (int)(DisplayWidth / DisplayHeight) * Texture.Height, (int)DisplayHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, destinationRectangle, Color.White);
        }

        public string SetRunerAction
        {
            set
            {
                RunnerAction = value;
            }
        }

        void Move()
        {
            if (x <= Texture.Width - (DisplayWidth / DisplayHeight) * Texture.Height)
            {
                if (RunnerAction == "Right")
                {
                    x += 2;
                }
            }
        }

        public int GetPositionX
        {
            get
            {
                return x;
            }
        }

        public int GetWidth
        {
            get
            {
                return (int)(DisplayWidth / DisplayHeight) * Texture.Height;
            }

        }

        public int X
        {
            set
            {
                x = value;
            }
            get
            {
                return x;
            }
        }
    }
}
