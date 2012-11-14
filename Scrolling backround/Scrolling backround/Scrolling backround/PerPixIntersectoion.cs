using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Scrolling_backround
{
    class PerPixIntersectoion
    {
       public static bool IntersectPixel(Rectangle Rect1, Color[] Data1, Rectangle Rect2, Color[] Data2)
        {
            // top of the lower rect
            int top = Math.Max(Rect1.Top, Rect2.Top);

            // lowest part of the upper rectangle
            int bottom = Math.Min(Rect1.Bottom, Rect2.Bottom);

            // furthest rectangle to the right's left side
            int left = Math.Max(Rect1.Left, Rect2.Left);

            // closest rectangle to the lefts right
            int right = Math.Min(Rect1.Right, Rect2.Right);

            // note leave 1 pixel transparent boarder around image to be totally accrate currently 1 pixel off :/
            // checks for intersection between left and right, for loop does top bottom check
            if (left < right)
            {
                // checks intersecting section only
                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        //cycles through intersection only
                        Color color1 = Data1[(x - Rect1.Left) + (y - Rect1.Top) * Rect1.Width];
                        Color color2 = Data2[(x - Rect2.Left) + (y - Rect2.Top) * Rect2.Width];

                        // checks the alpha
                        if (color1.A != 0 && color2.A != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
