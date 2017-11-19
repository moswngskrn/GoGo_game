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
    class HealthBar
    {
        Texture2D Bar;
        Vector2 Position;
        Color color;
        float maxHealth;
        float percen;

        public void LoadContent(ContentManager Content)
        {
            Bar = Content.Load<Texture2D>("bar");
            maxHealth = 1000.0f;
        }
        public void Update(GameTime gameTime,Vector2 Position,float CurrentHealth)
        {
            this.Position = Position;
            percen = CurrentHealth / maxHealth;
            if (percen > 0.4)
            {
                color = Color.Green;
            }
            else
            {
                color = Color.Red;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Bar, new Rectangle((int)Position.X, (int)Position.Y, (int)(80*percen),5), color);
        }
    }
}
