using BattleCity.Gameplay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using VelcroPhysics.Dynamics;

namespace BattleCity.Core
{
    // Chứa những instance duy nhất trong game dùng thay cho singleton.
    public static class Application
    {
        public static GraphicsDeviceManager GraphicsDeviceManager { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static ContentManager Content { get; set; }
        public static Input Input { get; set; }

        public static Scheduler Scheduler { get; set; }
        public static DrawWorld DrawWorld { get; set; }
        public static AnimateWorld AnimateWorld { get; set; }
        public static WorldSimulator WorldSimulator { get; set; }

        public static BoxingViewportAdapter ViewportAdapter { get; set; }
        public static Camera2D MainCamera { get; set; }

        public static ContentLoader ContentLoader { get; set; }


        public static double FrameRate { get; set; } = 0;
        public static int TargetFrameRate = 62;

        public const int ClientWidth = 1280;
        public const int ClientHeight = 720;
    }
}
