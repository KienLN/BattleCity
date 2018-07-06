using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleCity.Core
{
    public class AnimatedSprite : Drawable
    {
        #region Override
        protected override void OnAttach()
        {
            base.OnAttach();

        }

        protected override void OnDeattach()
        {
            base.OnDeattach();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_AnimateWorld.Remove(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_AnimateWorld.Add(this);
        }
        #endregion

        public AnimatedSprite(string textureName, Rectangle[] rects, int drawOrder, uint pixelPerUnit, bool visible = true) :
            base(textureName, rects[0], drawOrder, pixelPerUnit, visible)
        {
            Rects = rects;
            TotalFrame = Rects.Length;
        }

        public void Reset()
        {
            CurrentIndex = 0;
            Ended = false;
            TotalTime = 0;
        }

        public int CurrentIndex { get; set; } = 0;
        public double TimePerFrame { get; set; } = 0.25;
        public double TotalTime { get; set; } = 0;
        public bool Ended { get; set; } = false;
        public bool Looped { get; set; } = true;


        public int TotalFrame { get; private set; }
        public Rectangle[] Rects { get; }

        static AnimateWorld m_AnimateWorld = TickManager.FindAs<AnimateWorld>();
    }
}
