using BattleCity.Core;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace BattleCity.Gameplay.GameObject
{
    public class Bullet : Actor, ICollisionHandler
    {
        //protected override void OnDestroy()
        //{

        //}

        //protected override void OnDisable()
        //{
        //}

        //protected override void OnEnable()
        //{
        //}

        protected override void OnStart()
        {
            Console.WriteLine("Bullet start.");
        }

        //protected override void OnTick()
        //{

        //}

        //public void Move()
        //{

        //    switch (Transform.Rotation)
        //    {
        //        case 0:
        //            m_HitBox.Velocity = new Vector2(0, Speed);
        //            break;
        //        case 90:
        //            m_HitBox.Velocity = new Vector2(-Speed, 0);
        //            break;
        //        case 270:
        //            m_HitBox.Velocity = new Vector2(Speed, 0);
        //            break;
        //        case 180:
        //            m_HitBox.Velocity = new Vector2(0, -Speed);
        //            break;
        //        default:
        //            break;
        //    }

        //}

        public static void Fire(Bullet bullet, Transform transform, float offset = 0.75f)
        {
            bullet.Transform.Position = transform.Position;
            bullet.Transform.Rotation = transform.Rotation;
            switch ((int)bullet.Transform.Rotation)
            {
                case 0:
                    bullet.m_HitBox.Velocity = new Vector2(0, bullet.Speed);
                    bullet.Transform.Position += new Vector2(0, offset);
                    break;
                case 90:
                    bullet.m_HitBox.Velocity = new Vector2(-bullet.Speed, 0);
                    bullet.Transform.Position += new Vector2(-offset, 0);
                    break;
                case 270:
                    bullet.m_HitBox.Velocity = new Vector2(bullet.Speed, 0);
                    bullet.Transform.Position += new Vector2(offset, 0);
                    break;
                case 180:
                    bullet.m_HitBox.Velocity = new Vector2(0, -bullet.Speed);
                    bullet.Transform.Position += new Vector2(0, -offset);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public void OnColisionEnter(Actor other)
        {
            if (other.GetType() == typeof(BrickWall))
            {
                Active = false;
            }
        }
        public void OnColisionExit(Actor other)
        {

        }

        public Bullet()
        {
            m_Drawable = Application.ContentLoader.CreateDrawable("Bullet");
            m_Drawable.Owner = this;

            m_HitBox = new HitBox(new Vector2(0.25f), false, true);
            m_HitBox.Owner = this;
        }

        protected Drawable m_Drawable;
        protected HitBox m_HitBox;
        public float Speed { get; private set; } = 20;
    }
}
