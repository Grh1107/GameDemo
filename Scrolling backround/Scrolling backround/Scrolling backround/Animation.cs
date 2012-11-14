using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace AnimateChar
{
    class Animation
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public Vector2 Position;
        Vector2 Origin;
        public Vector2 Velocity;
        public KeyboardState pastKey;

        int currentFrame;
        public int frameHeight;
        public int frameWidth;
        int TotalSpriteAnimations = 7;
        int Ground = 450;
        bool hasJumped = true; 
        int ScreenHeight = 500;
        int ScreenWidth = 800;

        float timer;
        float interval = 50;

        SoundEffect jumpS;
        
        public static List<Color[]> spriteTextureDataL = new List<Color[]>();

        public Animation(Texture2D newTexture, Vector2 newPosition, int newFrameHeight, int newFrameWidth, SoundEffect J = null)
        {
            texture = newTexture;
            Position = newPosition;
            frameHeight = newFrameHeight;
            frameWidth = newFrameWidth;
            jumpS = J;
            // PerPixelCollision Data
            Color[] textureData = new Color[texture.Width * texture.Height]; 
            texture.GetData(textureData);
            if (spriteTextureDataL.Count == 0)
            {
                // cycles through each frame
                for (int Fr = 0; Fr <= TotalSpriteAnimations; Fr++)
                {
                    Color[] frame = new Color[frameWidth * frameHeight];
                    //top to bottom
                    for (int H = 0; H < texture.Height; H++)
                    {
                        // frame pos to end frame width
                        int RowPix = 0;
                        for (int W = frameWidth * Fr; W < (frameWidth * (Fr + 1)); W++)
                        {
                            frame[(H * frameWidth) + RowPix] = textureData[W + (H * texture.Width)];
                            RowPix++;
                        }
                    }
                    spriteTextureDataL.Add(frame);
                }
            }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, rectangle, Color.White, 0f, Origin, 1.0f, SpriteEffects.None, 0);
        }
        
        public void Update(GameTime gameTime)
        {
            // rectangle cycles trhough the picture, the pos of rectangle on img, is equal to width sq * which frame
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            Origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            
            
            
            Position += Velocity;
            Grav();
 
            if (Position.X <= 0)
            {
                Position.X = ScreenWidth; 
            }
            else if (Position.X >= ScreenWidth)
            {
                Position.X = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && !pastKey.IsKeyDown(Keys.Left))
            {
                AnimateRight(gameTime);
                Velocity.X = 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                AnimateLeft(gameTime);
                Velocity.X = -3;
            }
            else
            {
                Velocity.X = 0;
            }
            pastKey = Keyboard.GetState();
        }
        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame = (currentFrame+ 1) % 3;
                timer = 0;

            }
        }

        public void AnimateLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                //currentFrame = (currentFrame++) % TotalSpriteAnimations;
                currentFrame++;
                timer = 0;

                if (currentFrame > TotalSpriteAnimations || currentFrame < 4)
                {
                    currentFrame = 4;
                }
            }
        }

        public void Grav()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && !hasJumped)
            {
                 Velocity.Y = -5f;
                Position.Y -= 10f;
                hasJumped = true;
                if (jumpS != null)
                {
                    jumpS.Play();
                }
            }

            if (hasJumped)
            {
                //float i = 1;  // should probably be declared above no purpose looks like 
                Velocity.Y += 0.15f;// *i;
            }

            // if the position of the bottom of the image is 450 or greater hit ground
            if (Position.Y > Ground)
            {
                hasJumped = false;
                Velocity.Y = 0f;
            }
        }

    }
}
