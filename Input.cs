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
    class Input
    {
        KeyboardState CurrentKS, PrevKS;
        MouseState CurrentMS, PrevMS;
        protected void UpdateInput()
        {
            PrevKS = CurrentKS;
            CurrentKS = Keyboard.GetState();

            PrevMS = CurrentMS;
            CurrentMS = Mouse.GetState();
        }

        protected Vector2 MousePosition
        {
            get
            {
                return new Vector2(CurrentMS.Position.X,CurrentMS.Position.Y);
            }
        }

        protected bool LeftClickButton()
        {
            if(CurrentMS.LeftButton == ButtonState.Pressed && PrevMS.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        protected bool RightClickButton()
        {
            if (CurrentMS.RightButton == ButtonState.Pressed && PrevMS.RightButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        protected bool IsKeyDown(Keys key)
        {
            if (CurrentKS.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        protected bool IsKeyUp(Keys key)
        {
            if (CurrentKS.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        protected bool IsKeyDownUp(Keys key)
        {
            if(PrevKS.IsKeyDown(key) && CurrentKS.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }
    }
}
