using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoGo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int CurrentLevel;

        Level1 level11;
        Level1 level12;
        Level1 level13;
        PasswordGame level14;

        //Display
        float DisplayWidth, DisplayHeight;

        //Jigsaw
        Jigsaw jig;

        //GameWorld
        GameWorld Lavel1;

        //score
        SpriteFont ShowText;
        int score = 0;
        int power = 0;

        //Massage1;
        MassageBox massage1;
        MassageBox massage2;
        MassageBox massage3;
        float elapsed, interval;
        int currentMassege;
        int NextLevel;
        slidePicture s1, s2, s3;
        Texture2D Pic1, Pic2, Pic3;

        EndSeen end;

        KeyboardState KS;

        bool showEnd = false;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            CurrentLevel = 1;
            NextLevel = 11;


            //Display
            DisplayWidth = GraphicsDevice.Viewport.Width;
            DisplayHeight = GraphicsDevice.Viewport.Height;

            //Level
            level11 = new Level1((int)DisplayWidth,(int)DisplayHeight,6);
            level12 = new Level1((int)DisplayWidth,(int)DisplayHeight,8);
            level13 = new Level1((int)DisplayWidth, (int)DisplayHeight, 12);
            level14 = new PasswordGame((int)DisplayWidth, (int)DisplayHeight);

            //GameWorld
            Lavel1 = new GameWorld(DisplayWidth, DisplayHeight);


            //Massage
            massage1 = new MassageBox();
            massage2 = new MassageBox();
            massage3 = new MassageBox();
            elapsed = 0;
            interval = 30;
            currentMassege = 0;           

            //jigsaw
            jig = new Jigsaw();

            end = new EndSeen();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //GameWorld
            Lavel1.LoadContent(Content);

            //Level11
            level11.LoadContent(Content, "IMG_8264");
            level12.LoadContent(Content, "IMG_8302");
            level13.LoadContent(Content, "IMG_8322");
            level14.LoadContent(Content);

            //Massage
            massage1.LoadContent(Content, "อะไรเอ่ย หัวเป็นหนามถามไม่พูด?");
            massage2.LoadContent(Content, "อะไรเอ่ย นารีมีรู เพชรสีชมพูคาหูนารี?");
            massage3.LoadContent(Content, "อะไรเอ่ย นั่งก็ห้อย ยืนก็ห้อย ร้อยทั้งร้อยเป็นของผู้ชาย?");
            Pic1 = Content.Load<Texture2D>("m1");
            Pic2 = Content.Load<Texture2D>("m2");
            Pic3 = Content.Load<Texture2D>("m3");
            s1 = new slidePicture();
            s2 = new slidePicture();
            s3 = new slidePicture();
            s1.LoadContent(Content, "m1");
            s2.LoadContent(Content, "m2");
            s3.LoadContent(Content, "m3");

            jig.LoadContent(Content);

            ShowText = Content.Load<SpriteFont>("Massege");

            end.LoadContent(Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KS = Keyboard.GetState();
            
            score = Lavel1.score + level12.score+ level13.score+ level13.score;
            power = Lavel1.Power;
            if (Lavel1.CheckTuchBox())
            {
                CurrentLevel = NextLevel;
                Lavel1.SetPositionXRunner = 100f;
                Lavel1.isEndSeen = false;
                Lavel1.AddBackgrounds(0);
                Lavel1.SetPosYBox = -50;
            }
            if(CurrentLevel == 1)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                //GameWorld
                Lavel1.Update(gameTime);

                //Massege
                ChangeMassage(gameTime);
                UpdateMassage(gameTime);
                jig.Update();
                CheckPlayerTouchJigsaw();
                if (s1.Active)
                {
                    s1.Update();
                }
                if (s2.Active)
                {
                    s2.Update();
                }
                if (s3.Active)
                {
                    s3.Update();
                }

            }
            if(CurrentLevel == 11)
            {
                level11.Update(gameTime);
                
                if (!level11.Active)
                {
                    jig.havej1 = true;
                    CurrentLevel = 1;
                    NextLevel++;
                }
                
            }
            if (CurrentLevel == 12)
            {
                level12.Update(gameTime);
                if (!level12.Active)
                {
                    jig.havej2 = true;
                    CurrentLevel = 1;
                    NextLevel++;
                }
            }
            if (CurrentLevel == 13)
            {
                level13.Update(gameTime);
                if (!level13.Active)
                {
                    jig.havej3 = true;
                    CurrentLevel = 1;
                    NextLevel++;
                }
            }
            if (CurrentLevel == 14)
            {
                level14.Update();
                if (level14.Active)
                {
                    jig.havej4 = true;
                    CurrentLevel = 1;
                    NextLevel++;
                }
            }

            if (NextLevel == 15)
            {
                if (KS.IsKeyDown(Keys.Space))
                {
                    showEnd = true;
                    CurrentLevel = -1;
                }
            }
            if (showEnd)
            {
                end.Update();
            }


            base.Update(gameTime);
        }


        void UpdateMassage(GameTime gameTime)
        {
            if (massage1.Active)
            {
                massage1.Update(gameTime);
            }
            if (massage2.Active)
            {
                massage2.Update(gameTime);
            }
            if (massage3.Active)
            {
                massage3.Update(gameTime);
            }
        }

        void CheckTouch()
        {
            Rectangle rectangle1, rectangle2;


            //Runner and bulletMonter
            // for(int i=0;i<)

        }

        void ChangeMassage(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsed >= interval)
            {
                currentMassege += 1;
                if (currentMassege == 1)
                {
                    s1.Active = true;
                    massage1.Active = true;
                }
                if (currentMassege == 2)
                {
                    s2.Active = true;
                    massage2.Active = true;
                }
                if (currentMassege == 3)
                {
                    s3.Active = true;
                    massage3.Active = true;
                }
                elapsed = 0;
            }
            
        }

        void CheckPlayerTouchJigsaw()
        {
            Rectangle Rect1, Rect2;
            Rect1 = new Rectangle((int)Lavel1.GetPositionRunner.X - 30, (int)Lavel1.GetPositionRunner.Y - 40, 60, 80);
            for(int i = jig.X.Count-1; i>=0; i--)
            {
                Rect2 = new Rectangle(jig.X[i], 400, 50, 50);
                if (Rect1.Intersects(Rect2))
                {
                    jig.X[i] = -50;
                    if (i == 0)
                    {
                        jig.havej1 = true;
                    }
                    if (i == 1)
                    {
                        jig.havej2 = true;
                    }
                    if (i == 2)
                    {
                        jig.havej3 = true;
                    }
                    if (i == 3)
                    {
                        jig.havej4 = true;
                    }
                }
            }
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if(CurrentLevel == 1)
            {
                //GameWorld
                Lavel1.Draw(spriteBatch);

                //Massage
                if (massage1.Active)
                {
                    massage1.Draw(spriteBatch);
                }
                if (massage2.Active)
                {
                    massage2.Draw(spriteBatch);
                }
                if (massage3.Active)
                {
                    massage3.Draw(spriteBatch);
                }

                jig.Draw(spriteBatch);
            }

            if (CurrentLevel == 11)
            {
                level11.Draw(spriteBatch);
            }
            if (CurrentLevel == 12)
            {
                level12.Draw(spriteBatch);
            }
            if(CurrentLevel == 13)
            {
                level13.Draw(spriteBatch);
            }
            if(CurrentLevel == 14)
            {
                level14.Draw(spriteBatch);
            }
            
            spriteBatch.DrawString(ShowText, "Score : " + score+"\nPower :"+power, Vector2.Zero, Color.Yellow);

            s1.Draw(spriteBatch);
            s2.Draw(spriteBatch);
            s3.Draw(spriteBatch);

            if (showEnd)
            {
                end.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
