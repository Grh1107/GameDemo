using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Scrolling_backround
{
    class SingleScreen
    {
        Texture2D Screen;
        Rectangle rectangle;

        public SingleScreen(Texture2D ScreenToDisplay, GraphicsDevice graphics)
        {
            Screen = ScreenToDisplay;
            rectangle = new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Screen, rectangle, Color.White);
        }
    }
}
