using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Scrolling_backround
{
    class HealthBar
    {
        Texture2D Health;
        Vector2 Position;
        Rectangle rectangle;

        public int HP = 100;

        public HealthBar(Texture2D H, Vector2 P)
        {
            Health = H;
            Position = P;
            rectangle = new Rectangle(0, 0, Health.Width, Health.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Health, Position, rectangle, Color.White);
        }
        public static HealthBar operator-(HealthBar H, int HitPecent)
        {
            double Percent = HitPecent/100.00;
            H.rectangle.Width -= (int)(Percent * H.Health.Width);
            if (H.rectangle.Width < 0)
            {
                H.rectangle.Width = 0;
            }
            H.HP = H.rectangle.Width;
            return H;    
        }
        public static HealthBar operator +(HealthBar H, int HitPecent)
        {
            double Percent = HitPecent / 100.00;
            H.rectangle.Width += (int)(Percent * H.Health.Width);
            if (H.rectangle.Width > H.Health.Width)
            {
                H.rectangle.Width = H.Health.Width;
            }
            H.HP = H.rectangle.Width;
            return H;
        }

    }
}
