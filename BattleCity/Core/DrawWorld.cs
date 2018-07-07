using BattleCity.Core;
using BattleCity.Gameplay.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Core
{
    public sealed class DrawWorld : ITickObject
    {
        #region Emty implementation
        void ITickObject.OnDestroy()
        {

        }

        void ITickObject.OnDisable()
        {

        }

        void ITickObject.OnEnable()
        {

        }

        void ITickObject.OnInit()
        {
        }

        void ITickObject.OnStart()
        {

        }
        #endregion


        public void Add(Drawable drawable)
        {
            m_DrawableList.Add(drawable);
            m_DrawableList = m_DrawableList.OrderBy(x => x.DrawOrder).ToList();
        }

        public void Remove(Drawable drawable)
        {
            m_DrawableList.Remove(drawable);
        }

        void ITickObject.OnTick()
        {
            Application.GraphicsDevice.Clear(Color.Black);

            Application.SpriteBatch.Begin(
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp,
                transformMatrix: Application.MainCamera.GetViewMatrix());

            // Draw từng drawable trong có game.
            foreach (var drawable in m_DrawableList)
            {
                if (drawable.Visible)
                {
                    Application.SpriteBatch.Draw(
                        Application.ContentLoader.TextureMap[drawable.TextureName],
                        new Vector2(drawable.Owner.Transform.Position.X, Application.ViewportAdapter.VirtualHeight - drawable.Owner.Transform.Position.Y),
                        drawable.Rect,
                        drawable.DrawColor,
                        drawable.Owner.Transform.Rotation * MathHelper.Pi / -180,
                        new Vector2(drawable.Rect.Width, drawable.Rect.Height) * 0.5f,
                        drawable.Owner.Transform.Scale / drawable.PixelPerUnit,
                        SpriteEffects.None,
                        0);
                }
            }


            Application.SpriteBatch.End();
        }

        List<Drawable> m_DrawableList = new List<Drawable>();
    }
}

