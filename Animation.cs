using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoGo
{
    class Animation
    {
        //ประกาศตัวแปร
        public Texture2D Texture;//รูปภาพ

        Vector2 Position;//ตำแหน่งของ frame
        Vector2 origin;//จุด origin ของ frame
        int frameWidth, frameHeight;//ความกว้ามของframe , ความสูงของframe 
        int frameCountVertical, frameCountHorizontal;//จำนวน frame ตามแนวนอน , จำนวน frame ตามแนวนอน 
        int frameVertical; public int frameHorizontal;//index ของ frame ในแนวนอน , index ของ frame ในแนวตั้ง

        Rectangle destinationRectangle, sourceRectangle;
        float rotation;
        public SpriteEffects spriteEffects = SpriteEffects.None;

        public bool Active;
        bool Looping;

        float elapsed, interval;

        public Animation(Texture2D texture,
                        int frameWidth, int frameHeight,
                        int frameCountVertical, int frameCountHorizontal,
                        int frameVertical, int frameHorizontal,
                        float interval,
                        bool looping)
        {
            Texture = texture;
            this.frameCountVertical = frameCountVertical;
            this.frameCountHorizontal = frameCountHorizontal;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameVertical = frameVertical;
            this.frameHorizontal = frameHorizontal;
            Looping = looping;
            this.interval = interval;
            elapsed = 0.0f;
            Active = true;
            origin = new Vector2(Texture.Width / frameCountVertical, Texture.Height / frameCountHorizontal) / 2;
        }

        public void Update(GameTime gameTime, Vector2 Position)
        {
            this.Position = Position;
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Active)
            {
                if (elapsed >= interval)
                {
                    if (frameVertical >= frameCountVertical)
                    {
                        if (!Looping)
                        {
                            Active = false;
                            frameVertical = 1;
                        }
                        else
                        {
                            frameVertical = 1;
                        }
                    }
                    else
                    {
                        frameVertical++;
                    }
                    elapsed = 0;
                }
            }
            destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(((Texture.Width / frameCountVertical) * (frameVertical - 1)),
                                            ((Texture.Height / frameCountHorizontal) * (frameHorizontal - 1)),
                                            Texture.Width / frameCountVertical,
                                            Texture.Height / frameCountHorizontal);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0.0f, origin, spriteEffects, 0.0f);
        }
    }
}
