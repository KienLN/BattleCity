using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleCity
{
    public class BattleCity : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static double FrameRate { get; private set; } = 0;

        public BattleCity()
        {
            graphics = new GraphicsDeviceManager(this);
            TargetElapsedTime = System.TimeSpan.FromSeconds(1d / 25d);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TickManager.Spawn<Tank>();
        }

        protected override void UnloadContent()
        {
            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);

            FrameRate = 1.0 / gameTime.ElapsedGameTime.TotalSeconds;
            TickManager.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
