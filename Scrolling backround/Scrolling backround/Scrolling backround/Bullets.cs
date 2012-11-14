using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdvancedMovements
{
    class Bullets
    {
        public Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;

        public bool Hit = false;

        static public Color[] bulletTextureData;

        public bool isVisible;

        public Bullets(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = false;
            // should only be created once 
            if (bulletTextureData == null)
            {
                bulletTextureData = new Color[texture.Width * texture.Height];
                texture.GetData(bulletTextureData);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }

    }
}
