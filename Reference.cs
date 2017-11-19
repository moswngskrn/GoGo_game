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
    class Reference
    {
        Vector2 Origin;
        float DisplayWidth, DisplayHeight;

        public Vector2 SetOrigin{
            set
            {
                Origin = value;
            }
        }

        public void Initialize()
        {

        }

        public void SetDisplay(float DisplayWidth,float DisplayHeight)
        {
            this.DisplayWidth = DisplayWidth;
            this.DisplayHeight = DisplayHeight;
        }

    }
}
