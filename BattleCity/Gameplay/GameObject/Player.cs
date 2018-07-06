using BattleCity.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BattleCity.Gameplay.GameObject
{
    public class Player : Tank
    {
        protected override void OnTick()
        {
            base.OnTick();
            if (Application.Input.IsKeyDown(Keys.A))
            {
                m_HitBox.Velocity = new Vector2(-moveSpeed, 0);
                Transform.Rotation = 90;
            }
            else if (Application.Input.IsKeyDown(Keys.D))
            {
                m_HitBox.Velocity = new Vector2(moveSpeed, 0);
                Transform.Rotation = 270;
            }
            else if (Application.Input.IsKeyDown(Keys.W))
            {
                m_HitBox.Velocity = new Vector2(0, moveSpeed);
                Transform.Rotation = 0;
            }
            else if (Application.Input.IsKeyDown(Keys.S))
            {
                m_HitBox.Velocity = new Vector2(0, -moveSpeed);
                Transform.Rotation = 180;
            }
            else
            {
                m_HitBox.Velocity = new Vector2(0, 0);
            }

            if (Application.Input.IsKeyPressed(Keys.F))
            {
            }

            if (Application.Input.IsKeyPressed(Keys.Space))
            {
                //if (m_Bullet == null)
                //    m_Bullet = Bullet.Fire(Transform);
                //else
                //{
                //    m_Bullet.Active = true;
                //    Bullet.Fire(m_Bullet, Transform);
                //}
                Fire();
                //Active = true;
            }
        }

        Bullet m_Bullet;
        float moveSpeed = 12f;
    }
}
