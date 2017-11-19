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
    class PasswordGame:Input
    {
        bool Ans0, Ans1, Ans2, Ans3;
        List<int> Ans;
        int displayWidth, displayHeight;
        Texture2D background;
        SpriteFont Text;
        Input input;
        string TheResult;
        public bool TrueAns = false;

        public bool Active;

        string TheAnswers = "0365";

        public PasswordGame(int displayWidth, int displayHeight)
        {
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
            Active = true;
        }

        void ResetAns()
        {
            Ans0 = false;
            Ans1 = false;
            Ans2 = false;
            Ans3 = false;
            for (int i = Ans.Count - 1; i >= 0; i--)
            {
                Ans.RemoveAt(i);
            }
        }

        void checkKey()
        {
            if (!Ans0)
            {
                if (AddAns())
                {
                    Ans0 = true;
                    return;
                }
            }
            if (!Ans1)
            {
                if (AddAns())
                {
                    Ans1 = true;
                    return;
                }
            }
            if (!Ans2)
            {
                if (AddAns())
                {
                    Ans2 = true;
                    return;
                }
            }
            if (!Ans3)
            {
                if (AddAns())
                {
                    Ans3 = true;
                    return;
                }
            }
        }

        bool AddAns()
        {
            if (IsKeyDownUp(Keys.NumPad0) || checkClick() == 0)
            {
                Ans.Add(0);
                return true;
            }
            if (IsKeyDownUp(Keys.NumPad1) || checkClick() == 1)
            {
                Ans.Add(1);
                return true;
            }
            if (IsKeyDownUp(Keys.NumPad2) || checkClick() == 2)
            {
                Ans.Add(2);
                return true;
            }
            if (IsKeyDownUp(Keys.NumPad3) || checkClick() == 3)
            {
                Ans.Add(3);
                return true;

            }
            if (IsKeyDownUp(Keys.NumPad4) || checkClick() == 4)
            {
                Ans.Add(4);
                return true;
            }
            if (IsKeyDownUp(Keys.NumPad5) || checkClick() == 5)
            {
                Ans.Add(5);
                return true;
            }
            if (IsKeyDownUp(Keys.NumPad6) || checkClick() == 6)
            {
                Ans.Add(6);
                return true;
            }
            if (IsKeyDownUp(Keys.NumPad7) || checkClick() == 7)
            {
                Ans.Add(7);
                return true;
            }
            if (IsKeyDownUp(Keys.NumPad8) || checkClick() == 8)
            {
                Ans.Add(8);
                return true;
            }
            if (IsKeyDownUp(Keys.NumPad9) || checkClick() == 9)
            {
                Ans.Add(9);
                return true;
            }
            return false;
        }

        bool IsAnsTrue()
        {
            return Ans1 && Ans2 && Ans3 && Ans0;
        }


        string AnsTOString()
        {
            string result = "";
            for (int i = 0; i < Ans.Count; i++)
            {
                result += Ans[i].ToString();
            }
            return result;
        }

        public void LoadContent(ContentManager Content)
        {
            input = new Input();
            Text = Content.Load<SpriteFont>("Massege");
            Ans = new List<int>();
            background = Content.Load<Texture2D>("Code");
            Active = false;
        }

        int checkClick()
        {
            int x = 1;
            for (int i = 2; i >= 0; i--)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((MousePosition.X >= 500 + 45 * j && MousePosition.X <= 540 + 45 * j) &&
                        (MousePosition.Y >= 180 + 45 * i && MousePosition.Y <= 220 + 45 * i))
                    {
                        if (LeftClickButton())
                        {
                            return x;
                        }
                    }
                    x++;
                }
            }
            if((MousePosition.X >= 500 && MousePosition.X <= 500 + 40)&&
               (MousePosition.Y >= 315 && MousePosition.Y <= 315 + 40))
            {
                if (LeftClickButton())
                {
                    return 0;
                }
            }
            return -1;
        }

        public void Update()
        {
            UpdateInput();

            if (IsKeyDownUp(Keys.Back))
            {
                if (Ans.Count > 0)
                {
                    if (Ans.Count - 1 == 0)
                    {
                        Ans0 = false;
                    }
                    if (Ans.Count - 1 == 1)
                    {
                        Ans1 = false;
                    }
                    if (Ans.Count - 1 == 2)
                    {
                        Ans2 = false;
                    }
                    if (Ans.Count - 1 == 3)
                    {
                        Ans3 = false;
                    }
                    Ans.RemoveAt(Ans.Count - 1);
                }
            }
            if (Ans.Count < 4 && !TrueAns)
            {
                checkKey();
            }
            TheResult = AnsTOString();
            if (IsKeyDownUp(Keys.Enter))
            {
                if (Ans.Count >= 4)
                {
                    if (IsAnsTrue())
                    {
                        ResetAns();
                        if (TheResult == TheAnswers)
                        {
                            TheResult = "True!";
                            TrueAns = true;
                            Active = true;
                        }
                        else
                        {
                            TheResult = "Fail!";
                            Active = false;
                        }

                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(20, 20, displayWidth - 40, displayHeight - 40), new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.5f)));
            spriteBatch.DrawString(Text, TheResult, new Vector2(510, 125), Color.Red);
            int x = 1;

            if (TrueAns)
            {
                spriteBatch.DrawString(Text, "True", new Vector2(510, 125), Color.Green);
            }
        }
    }
}
