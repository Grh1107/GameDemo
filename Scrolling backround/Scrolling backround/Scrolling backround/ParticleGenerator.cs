using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ParticleGenerator
{
    class ParticleGeneratorC
    {
        Texture2D texture;
        float spawnWidth;
        float denisity;

        List<Raindrops> rainDrops = new List<Raindrops>();

        float timer;
        Random rand1;
        Random rand2;
        float rainScale;
        float WindDirection;

        public ParticleGeneratorC(Texture2D newTexture, float newSpawnWidth, float newDensity, float Scale = 1f, float Wind = 1f)
        {
            texture = newTexture;
            spawnWidth = newSpawnWidth;
            denisity = newDensity;
            rand1 = new Random();
            rand2 = new Random(320);
            rainScale = Scale;
            WindDirection = Wind;
        }

        public void CreateParticle()
        {
            rainDrops.Add(new Raindrops(texture, new Vector2(-50 + (float)rand1.NextDouble()* spawnWidth, 0),
                new Vector2(WindDirection, rand2.Next(5, 8)), rainScale));
        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer > 0)
            {
                timer -= 1f / denisity;
                CreateParticle();
            }

            for (int i = 0; i < rainDrops.Count; i++)
            {
                rainDrops[i].Update();
                if (rainDrops[i].Position.Y > graphics.Viewport.Height)
                {
                    rainDrops.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Raindrops raindrop in rainDrops)
            {
                raindrop.Draw(spriteBatch);
            }
        }
    }
}
