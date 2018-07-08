using BattleCity.Core;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace BattleCity.Gameplay.GameObject
{
    public class Bullet : Actor, ICollisionHandler
    {
        public static void Fire(Bullet bullet, Transform transform, float speed = 25, float offset = 1.25f)
        {
            bullet.Transform.Position = transform.Position;
            bullet.Transform.Rotation = transform.Rotation;
            bullet.Speed = speed;
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
            var type = other.GetType();
            if (type == typeof(Player) ||
                type == typeof(Enemy) ||
                type == typeof(Bullet) ||
                type == typeof(BrickWall) ||
                type == typeof(Bound) ||
                type == typeof(SteelWall))
            {
                Active = false;
            }
        }
        public void OnColisionExit(Actor other)
        {

        }

        public Bullet()
        {
            Application.ContentLoader.CreateDrawable("Bullet", 5).Owner = this;
            m_HitBox = new HitBox(new Vector2(0.3f), false, true);
            m_HitBox.Owner = this;
        }

        protected HitBox m_HitBox;
        float Speed { get; set; }
    }
}
