using System;
using AdvancedMovements;
using AnimateChar;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleGenerator;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Scrolling_backround
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Scrolling ScrollA;
        Scrolling ScrollB;
        Animation Character;
        Sentry Turret;

        HealthBar Health;

        KeyboardState pastKey;

        ParticleGeneratorC frontRain;
        ParticleGeneratorC backRain;

        Song RainSound;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            RainSound = Content.Load<Song>("rain");
            MediaPlayer.Play(RainSound);
            MediaPlayer.Volume = .25f;
            MediaPlayer.IsRepeating = true;
            frontRain = new ParticleGeneratorC(Content.Load<Texture2D>("raindrop"), graphics.GraphicsDevice.Viewport.Width, 25);
            backRain = new ParticleGeneratorC(Content.Load<Texture2D>("raindrop"), graphics.GraphicsDevice.Viewport.Width, 66, .7f, 2f);
            Health = new HealthBar(Content.Load<Texture2D>("HealthBar"), new Vector2(30));
            Vector2 Location = new Vector2(0.0f, 0.0f);
            ScrollA = new Scrolling (Content.Load<Texture2D>("Background/ScrollTest"), Location);
            Vector2 Location2 = new Vector2 (ScrollA.texture.Width, 0.0f);
            ScrollB = new Scrolling(Content.Load<Texture2D>("Background/ScrollTest2"), Location);
            // TODO: use this.Content to load your game content here
            Character = new Animation(Content.Load<Texture2D>("Background/Spritesheet"), 
                new Vector2(50, 450), 47, 44);
            Turret = new Sentry(Content.Load<Texture2D>("Enemys/Sentry"), Content.Load<Texture2D>("Enemys/Bullets"), 
                Character.Position, new Vector2(400, 100), Content.Load<SoundEffect>("shot"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            frontRain.Update(gameTime, graphics.GraphicsDevice);
            backRain.Update(gameTime, graphics.GraphicsDevice);
            
            Bounds();
            FireMechanism(gameTime);
            ScrollA.update(); 
            ScrollB.update();
            Character.Update(gameTime);
            Turret.CharDetails(Character.Position, Character.Velocity);
            Turret.Update();
            Turret.UpdateBullets();
            GotShot();
            base.Update(gameTime);
        }

        float ShotTime = 0;
        float ShotInterval = 1;
        Random Rand = new Random();
        public void FireMechanism(GameTime gameTime)
        {
            ShotTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (ShotTime > ShotInterval)
            {
                Turret.Shoot();
                ShotTime = 0;
                // ~1/5 times changes the shot interval 
                if (Rand.Next(0, 5) == 4)
                {
                    ShotInterval = (float)(Rand.Next(20, 100) / 100.0);
                }
            }

        }

        public void GotShot()
        {
            
            foreach (Bullets bullet in Turret.bullets)
            {
                bool GS = false;
                if (!bullet.Hit)
                {
                    Rectangle BRect = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, Turret.BulletTexture.Width, Turret.BulletTexture.Height);
                    Rectangle PRect = new Rectangle((int)Character.Position.X - Character.frameWidth / 2, (int)Character.Position.Y - Character.frameHeight / 2, Character.frameWidth, Character.frameHeight);
                    GS = PerPixIntersectoion.IntersectPixel(BRect, Bullets.bulletTextureData,
                        PRect, Animation.spriteTextureDataL[Character.CurrentFrame]);
                    if (GS)
                    {
                        bullet.Hit = true;
                        Health -= 5;
                        Console.WriteLine("Ouchies");
                    }
                }
            }
        }

        public void Bounds()
        {
            if (ScrollA.rectangle.X + ScrollA.texture.Width <= 0)
            {
                ScrollA.rectangle.X = ScrollB.rectangle.X + ScrollB.texture.Width;
            }
            else if (ScrollA.rectangle.X >= 0)
            {
                ScrollB.rectangle.X = ScrollA.rectangle.X - ScrollB.texture.Width;
            }
            if (ScrollB.rectangle.X + ScrollB.texture.Width <= 0)
            {
                ScrollB.rectangle.X = ScrollA.rectangle.X + ScrollA.texture.Width;
            }
            else if (ScrollB.rectangle.X >= 0)
            {
                ScrollA.rectangle.X = ScrollB.rectangle.X - ScrollA.texture.Width;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            ScrollA.Draw(spriteBatch);
            ScrollB.Draw(spriteBatch);
            backRain.Draw(spriteBatch);
            Character.Draw(spriteBatch);
            Turret.Draw(spriteBatch);
            frontRain.Draw(spriteBatch);
            Health.Draw(spriteBatch);
            /*Texture2D test = Content.Load<Texture2D>("Black tile");
            Rectangle PRect = new Rectangle((int)Character.Position.X - Character.frameWidth / 2, (int)Character.Position.Y - Character.frameHeight / 2, Character.frameWidth, Character.frameHeight);
            spriteBatch.Draw(test, PRect, Color.Orange);

            Texture2D testA = Content.Load<Texture2D>("Black tile");
            foreach (Bullets bullet in Turret.bullets)
            {
                Rectangle BRect = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, Turret.BulletTexture.Width, Turret.BulletTexture.Height);
                spriteBatch.Draw(testA, BRect, Color.Orange);
            }*/
            
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
