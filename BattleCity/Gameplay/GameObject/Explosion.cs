using BattleCity.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Gameplay.GameObject
{
    class Explosion : Actor
    {
        public Explosion()
        {
            m_Sprite = Application.ContentLoader.CreateAnimatedSprite("Explosion1", "Explosion2", "Explosion3");
            m_Sprite.Looped = false;
            m_Sprite.TimePerFrame = 0.04;
            m_Sprite.Owner = this;

            m_HitBox = new HitBox(Vector2.One * 0.75f, false, true);
            m_HitBox.Owner = this;
        }

        protected override void OnStart()
        {
            Console.WriteLine("Explositon start.");
        }

        protected override void OnEnable()
        {
            m_Sprite.Reset();
        }

        protected override void OnTick()
        {
            if (m_Sprite.Ended) Active = false;
        }

        AnimatedSprite m_Sprite;
        protected HitBox m_HitBox;
    }
}
