using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GoGo
{
    class Kunai
    {
        float DisplayWidth, DisplayHeight;

        Animation Texture;
        public Vector2 Position;
        string TurnOfRuner;

        public bool Active;

        public Kunai(float displayWidth, float displayHeight)
        {
            DisplayWidth = displayWidth;
            DisplayHeight = displayHeight;
        }

        public void LoadContent(ContentManager Content,string TrunOfPlayer)
        {
            Texture = new Animation(Content.Load<Texture2D>("ICY_Water2"), 50, 50, 5, 6, 1, 1, 150, true);
            Texture.Active = true;
            TurnOfRuner = TrunOfPlayer;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if(TurnOfRuner == "Left")
            {
                Position.X -= 5;
            }
            else
            {
                Position.X += 5;
            }
            Texture.Update(gameTime, Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture.Draw(spriteBatch);
        }
    }
}
