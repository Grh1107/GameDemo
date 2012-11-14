using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AdvancedMovements;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Scrolling_backround
{
    class Sentry
    {
        SpriteBatch spriteBatch;

        Texture2D spriteTexture;
        Rectangle spriteRectangle;
       public Texture2D BulletTexture;

        // centre of image
        Vector2 spriteOrigin;
        Vector2 SentryPosition;
        Vector2 spriteVelocity;
        Vector2 CenterImage;

        Vector2 CharLoc;
        Vector2 CharVelocity;

        SoundEffect shotsound;

        const float tangentialVelocity = 5f;
        float friction = 0.03f;

        float rotation;

        // aim calculation
        float OppositeL;
        float Adjacent;
        double HypoL;

        public List<Bullets> bullets = new List<Bullets>();
        KeyboardState pastKey;

        public Sentry(Texture2D SentryTexture, Texture2D Bull, Vector2 CharacterLocation, Vector2 SentryPos , SoundEffect S)
        {
            spriteTexture = SentryTexture;
            BulletTexture = Bull;
            CharLoc = CharacterLocation;
            SentryPosition = SentryPos;
            shotsound = S;
        }

        public void CharDetails(Vector2 NewCharPos, Vector2 CharVelo)
        {
            CharLoc = NewCharPos;
            CharVelocity = CharVelo;
        }

        public void UpdateBullets()
        {
            foreach (Bullets bullet in bullets)
            {
                // velocity should be updated in ratio
                bullet.position += bullet.velocity;
                // if the bullet distance is more that 500 it dissapears
                if (Vector2.Distance(bullet.position, SentryPosition) > 600)
                {
                    bullet.isVisible = false;

                }
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].isVisible)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shoot()
        {
            if (bullets.Count < 20)
            {
                shotsound.Play();
                Bullets newBullet = new Bullets(BulletTexture);
                newBullet.position = SentryPosition;//+ newBullet.velocity * 5f;
                newBullet.velocity = new Vector2(-(float)Math.Sin(rotation) * 5f, (float)Math.Cos(rotation) * 5f);
                float RiseRunRatio = (CharLoc.X - SentryPosition.X) / (CharLoc.Y - SentryPosition.Y);
                newBullet.velocity = new Vector2(RiseRunRatio, RiseRunRatio / RiseRunRatio) * 5f;
                newBullet.isVisible = true;
                bullets.Add(newBullet);
            }
        }

       public void Update()
       {
           spriteOrigin = new Vector2((spriteRectangle.Width / 2.0f), (spriteRectangle.Height / 3.0f) / 2.0f);
           spriteRectangle = new Rectangle((int)SentryPosition.X, (int)SentryPosition.Y, spriteTexture.Width, spriteTexture.Height);  
           OppositeL= (CharLoc.X-SentryPosition.X);
           Adjacent = (CharLoc.Y - SentryPosition.Y);
           // pythagoreanthyrim a^2 + b^2 = c^2;
           double cSq = (OppositeL*OppositeL)+(Adjacent*Adjacent);
           HypoL = Math.Sqrt(cSq);
           rotation = -(float)Math.Sin(OppositeL / HypoL);
       }
       
       public void Draw(SpriteBatch spriteBatch)
       {
           spriteBatch.Draw(spriteTexture, SentryPosition, null, Color.White, rotation, spriteOrigin, 1.0f, SpriteEffects.None, 0);
            foreach (Bullets bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

       }

    }


}
