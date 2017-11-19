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
    class Butterfly:Input
    {
        Animation butterfly;
        Vector2 Position;
        Vector2 Velocity;
        Texture2D mouse;

        public void LoadContent(ContentManager Content)
        {
            butterfly = new Animation(Content.Load<Texture2D>("Butterfly"),80,80, 14, 6, 1, 1, 150, true);
            mouse = Content.Load<Texture2D>("mouse");
        }

        public void Update(GameTime gameTime)
        {
            UpdateInput();
            CalculateAngle();
            Position += Velocity;
            if (Velocity.X < 0)
            {
                butterfly.spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                butterfly.spriteEffects = SpriteEffects.None;
            }
            butterfly.Update(gameTime, Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mouse,new Rectangle((int)MousePosition.X,(int)MousePosition.Y,20,20), Color.White);
            butterfly.Draw(spriteBatch);
        }



        //Function

        public Vector2 GetPosition
        {
            get
            {
                return Position;
            }
        }

        void CalculateAngle()
        {
            Vector2 direction;
            float destance;
            Velocity = Vector2.Zero;
            destance = CalculateDistance(MousePosition,Position);
            direction = MousePosition - Position;


            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            if (destance < 3)
            {
                Velocity += direction * destance;
            }
            else
            {
                Velocity += direction * 5;
            }

        }

        float CalculateDistance(Vector2 P1, Vector2 P2)
        {
            P1 = new Vector2(Math.Abs(P1.X), Math.Abs(P1.Y));
            P2 = new Vector2(Math.Abs(P2.X), Math.Abs(P2.Y));
            float X_deff, Y_deff, distance;
            X_deff = P1.X - P2.X;
            Y_deff = P1.Y - P2.Y;
            distance = (float)Math.Abs(Math.Pow(X_deff, 2) + Math.Pow(Y_deff, 2));
            return distance;
        }
    }
}
