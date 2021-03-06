using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Byte_Blast
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private CameraManager m_CameraManager;
        private HallwayManager m_HallwayManager;
        private SwipeManager m_SwipeManager;
        private MonsterManager m_MonsterManager;
        private float m_GameSpeed = 6.0f;

        #region Content Declerations

        Texture2D m_BackgoundSprite;
        Texture2D m_PlayerSprite;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            m_HallwayManager = new HallwayManager();
            m_SwipeManager = new SwipeManager();
            m_MonsterManager = new MonsterManager();
            m_CameraManager = new CameraManager(GraphicsDevice.Viewport);

            m_CameraManager.EnableTargetFollow((int)CameraManager.FollowState.SMOOTH);
            m_CameraManager.SetTarget(Vector2.Zero, 1.0f, 0.0f, 10.0f);
            m_CameraManager.SetCamera(new Vector2(0.0f, -200.0f), 0.075f, 0.0f);            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            TouchPanel.EnabledGestures = GestureType.Flick;

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

            // Load Game Content
            try
            {
                m_BackgoundSprite = Content.Load<Texture2D>("background");
                m_PlayerSprite = Content.Load<Texture2D>("player");
            }
            catch (Exception E)
            {
                Console.WriteLine("Fatal Error: " + E.Message);
            }

            m_HallwayManager.Load(Content);
            m_SwipeManager.Load(Content);
            m_MonsterManager.Load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
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

            float playspeed = m_GameSpeed;
            if (m_MonsterManager.IsConfronting())
                playspeed = 0.0f;

            Console.WriteLine(m_MonsterManager.IsConfronting());

            // TODO: Add your update logic here
            m_HallwayManager.Update(playspeed);
            m_CameraManager.Update();
            m_SwipeManager.Update();
            m_MonsterManager.Update(playspeed);

            if (m_SwipeManager.AttackReady())
            {
                m_MonsterManager.DamageMonster(5);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw Game
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_CameraManager.GetCamera().Transform);

            m_HallwayManager.Draw(spriteBatch);
            m_MonsterManager.Draw(spriteBatch);
            spriteBatch.Draw(m_PlayerSprite, new Vector2(20.0f, 170.0f), Color.White);
            spriteBatch.Draw(m_BackgoundSprite, new Vector2(-1024, -240), Color.White);

            spriteBatch.End();

            // Draw GUI
            spriteBatch.Begin();

            m_SwipeManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
