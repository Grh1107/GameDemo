using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Scrolling_backround
{
    class ScrollBackground
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public KeyboardState pastKey;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
    class Scrolling : ScrollBackground
    {
        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public Scrolling(Texture2D newTexture, Vector2 Location)
        {
            texture = newTexture;
            rectangle = new Rectangle ((int)Location.X, (int)Location.Y, texture.Width, texture.Height);
        }

        public void update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && !pastKey.IsKeyDown(Keys.Right))
            { rectangle.X += 3; }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {rectangle.X -= 3; }
            pastKey = Keyboard.GetState();
           
        }
    }
}
