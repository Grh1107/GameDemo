using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleGenerator
{
    class Raindrops
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;

        float Scale;

        public Vector2 Position
        {
            get {return position; }
        }

        public Raindrops(Texture2D T, Vector2 P, Vector2 V, float S)
        {
            texture = T;
            position = P;
            velocity = V;
            Scale = S;
        }

        public void Update()
        {
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(0), Scale , SpriteEffects.None, 0);
        }   
    }
}
