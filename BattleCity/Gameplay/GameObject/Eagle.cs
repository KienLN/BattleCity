using BattleCity.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Gameplay.GameObject
{
    class Eagle : Actor, ICollisionHandler
    {
        public Eagle()
        {
            m_HitBox = new HitBox(Vector2.One, true, true);
            m_Drawable = Application.ContentLoader.CreateDrawable("EagleNormal");
            m_HitBox.Owner = this;
            m_Drawable.Owner = this;
        }

        public void OnColisionEnter(Actor other)
        {
            if (other as Bullet != null)
            {
                m_Drawable.Owner = null;
                m_HitBox.Owner = null;
                m_Drawable = Application.ContentLoader.CreateDrawable("EagleDead");
                m_Drawable.Owner = this;
            }
        }

        public void OnColisionExit(Actor other)
        {
            
        }

        Drawable m_Drawable;
        HitBox m_HitBox;
    }
}
