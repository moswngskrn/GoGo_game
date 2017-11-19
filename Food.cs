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
    class Food
    {
        float DisplayWidth, DisplayHeight;

        Animation Gravity;
        Vector2 Position;

        public bool Active;

        string RunnerAction;

        public Food(float displayWidth, float displayHeight)
        {
            DisplayWidth = displayWidth;
            DisplayHeight = displayHeight;
        }

        public void LoadContent(ContentManager Content,Vector2 Position)
        {
            Gravity = new Animation(Content.Load<Texture2D>("Gravity"), 50, 50, 5, 4, 1, 1, 150, true);
            this.Position = Position;
            Active = true;
            Gravity.Active = true;
            
        }

        public void Update(GameTime gameTime)
        {
            Position.X -= 1;
            Move();
            Gravity.Update(gameTime, Position);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Gravity.Draw(spriteBatch);
        }

        void Move()
        {
            if (RunnerAction == "Right")
            {
                Position.X -= 2;
            }
        }

        public string SetRunerAction
        {
            set
            {
                RunnerAction = value;
            }
        }

        public Vector2 GetPosition
        {
            get
            {
                return Position;
            }
        }
    }
}
