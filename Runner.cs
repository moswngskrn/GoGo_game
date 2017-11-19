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
    class Runner:Input
    {
        ContentManager Content;
        float DisplayWidth, DisplayHeight;
        public int power;
        HealthBar healthBar;

        Animation animation;
        public Vector2 Position;
        int Width = 80, Height = 100;
        float vy, u, g, timer;
        bool isJump;
        string Turn = "Right";
        bool isEndSeen;
        Texture2D Jump, Attack, Jump_Attack, Jump_Throw, Slide,Run;

        string Action;

        public List<Kunai> Kunais;


        public Runner(float displayWidth, float displayHeight)
        {
            DisplayWidth = displayWidth;
            DisplayHeight = displayHeight;
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = Content;
            Jump = Content.Load<Texture2D>("Jump");
            Attack = Content.Load<Texture2D>("Attack");
            Jump_Attack = Content.Load<Texture2D>("Jump_Attack");
            Jump_Throw = Content.Load<Texture2D>("Jump_Throw");
            Run = Content.Load<Texture2D>("Run");
            Slide = Content.Load<Texture2D>("Slide");

            animation = new Animation(Run, 80, 100, 10, 1, 1, 1, 100, false);
            Position = new Vector2(DisplayWidth / 2, DisplayHeight - 30);

            u = -10;
            g = 20;
            timer = 0;

            Kunais = new List<Kunai>();
        }

        void AddKunai()
        {
            Kunai k = new Kunai(DisplayWidth, DisplayHeight);
            k.LoadContent(Content, Turn);
            k.Position = new Vector2(Position.X,Position.Y);
            Kunais.Add(k);
        }

        public void Update(GameTime gameTime)
        {
            if (!animation.Active)
            {
                animation.Texture = Run;
                Action = "";
            }

            UpdateInput();
            CheckKeys();

            if (IsKeyDownUp(Keys.P))
            {
                if (power > 300)
                {
                    AddKunai();
                    power -= 300;
                }
            }

            if (isJump)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                if (Position.Y < (DisplayHeight - (Height / 3)) && Position.Y > 0)
                {
                    vy = u + g * timer;
                    Position.Y += vy;
                }
                else
                {
                    Position.Y = DisplayHeight - Height / 2;
                    u = -10;
                    g = 20;
                    timer = 0;
                    isJump = false;
                    animation.Texture = Run;
                }
            }

            for(int i = 0; i < Kunais.Count; i++)
            {
                Kunais[i].Update(gameTime);
            }

            animation.Update(gameTime, Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = Kunais.Count - 1; i >= 0; i--)
            {
                Kunais[i].Draw(spriteBatch);
                if(!Kunais[i].Active)
                {
                    Kunais.RemoveAt(i);
                }
            }

            animation.Draw(spriteBatch);
        }
        public string GetAction
        {
            get
            {
                return Action;
            }
        }
        void CheckKeys()
        {
            if (IsKeyDown(Keys.Left))
            {
                animation.Active = true;
                animation.spriteEffects = SpriteEffects.FlipHorizontally;
                Action = "Left";
                Turn = "Left";
                if (Position.X >0)
                {
                    Position.X -= 2;
                }
            }
            if (IsKeyDown(Keys.Right))
            {
                animation.Active = true;
                animation.spriteEffects = SpriteEffects.None;
                Action = "Right";
                Turn = "Right";
                if (Position.X < DisplayWidth / 2 || isEndSeen)
                {
                    Position.X += 2;
                }
            }
            if (IsKeyDown(Keys.Up))
            {
                animation.Texture = Jump;
                animation.Active = true;
                Action = "Jump";
                isJump = true;
            }
            if (IsKeyDown(Keys.Down))
            {
                animation.Texture = Attack;
                animation.Active = true;
                Action = "Attack";
            }
            if(Action == "Jump" && IsKeyDown(Keys.P))
            {
                animation.Texture = Jump_Attack;
                animation.Active = true;
            }

            
        }
        public bool SetEndSeen
        {
            set
            {
                isEndSeen = value;
            }
        }
    }
}
