using BattleCity.Gameplay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;

namespace BattleCity.Core
{
    class BattleCity : Game
    {
        GraphicsDeviceManager graphics;

        public BattleCity()
        {
            graphics = new GraphicsDeviceManager(this);
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / Application.TargetFrameRate);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Application.ClientWidth;
            graphics.PreferredBackBufferHeight = Application.ClientHeight;
            //graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;

            Application.GraphicsDevice = GraphicsDevice;
            Application.SpriteBatch = new SpriteBatch(GraphicsDevice);

            Application.GraphicsDeviceManager = graphics;
            Application.Content = Content;

            var viewportAdapter = new BoxingViewportAdapter(
                Window,
                GraphicsDevice,
                graphics.PreferredBackBufferWidth / 16,
                graphics.PreferredBackBufferHeight / 16);

            Application.ViewportAdapter = viewportAdapter;
            Application.MainCamera = new Camera2D(viewportAdapter);


            TickManager.Spawn<StartScene>();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            Application.FrameRate = gameTime.ElapsedGameTime.TotalSeconds;
            TickManager.Update();
        }
    }
}

