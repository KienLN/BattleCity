using BattleCity.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Gameplay.GameObject
{
    public abstract class Tank : Actor, ICollisionHandler
    {
        public Tank()
        {
            m_AppearingSprite = Application.ContentLoader.CreateAnimatedSprite("Twinkle1", "Twinkle2", "Twinkle3", "Twinkle4", "Twinkle5", "Twinkle6");
            m_ShieldSprite = Application.ContentLoader.CreateAnimatedSprite("Shield1", "Shield2");
            (m_ShieldSprite as AnimatedSprite).TimePerFrame = 0.05;
            (m_AppearingSprite as AnimatedSprite).TimePerFrame = 0.05;
            m_HitBox = new HitBox(new Vector2(1.75f), false, false);

            SetUpPool();
        }

        public void SetUpPool()
        {
            m_ExplosionPool = new ObjectPool<Explosion>(() =>
            {
                Explosion explosion = Spawn<Explosion>(Vector2.Zero);
                explosion.OnStateChange += (ActorState state) =>
                {
                    if (state == ActorState.Inactive)
                    {
                        m_ExplosionPool.PutObject(explosion);
                    }
                };
                return explosion;
            });

            m_BulletPool = new ObjectPool<Bullet>(() =>
            {
                Bullet bullet = Spawn<Bullet>(Vector2.Zero);
                bullet.OnStateChange += (ActorState state) =>
                {
                    if (state == ActorState.Inactive)
                    {
                        var explosion = m_ExplosionPool.GetObject();
                        explosion.Active = true;
                        explosion.Transform.Position = bullet.Transform.Position;
                        m_BulletPool.PutObject(bullet);
                    }
                };
                return bullet;
            });
        }

        void Respawn()
        {
            m_ShieldSprite.Owner = null;
            m_HitBox.Owner = null;
            if (m_NormalSprite != null) m_NormalSprite.Owner = null;

            MoveSpeed = 8;
            m_BulletSpeed = 24;
            m_ShootCooldown = 0.6;
            m_CanShoot = false;
            m_HitPoint = 1;



            OnRespawn(ref m_NormalSprite, ref m_BulletSpeed, ref m_ShootCooldown, ref m_HitPoint);

            m_AppearingSprite.Owner = this;
            Active = true;

            Application.Scheduler.RemoveAllByTaskOwner(this);
            Application.Scheduler.Add(() =>
            {
                m_CanShoot = true;
                m_AppearingSprite.Owner = null;
                m_ShieldSprite.Owner = this;
                m_NormalSprite.Owner = this;
                m_HitBox.Owner = this;
                Application.Scheduler.Add(() =>
                {
                    m_ShieldSprite.Owner = null;
                }, SHIELD_DURATION, false);
            }, APPEAR_TIME, false);

            Transform = new Transform(RespawnTransform.Position, RespawnTransform.Rotation, RespawnTransform.Scale);
        }

        protected override void OnStart()
        {
            RespawnTransform = new Transform(Transform.Position, Transform.Rotation, Transform.Scale);
            Respawn();
        }

        protected abstract void OnRespawn(ref Drawable sprite, ref float bulletSpeed, ref double shootCooldown, ref int hitPoint);

        protected void Shoot()
        {
            if (m_CanShoot)
            {
                var bullet = m_BulletPool.GetObject();
                bullet.Active = true;
                Bullet.Fire(bullet, Transform, m_BulletSpeed);
                m_CanShoot = false;
                Application.Scheduler.Add(() => { m_CanShoot = true; }, m_ShootCooldown, false);
            }
        }

        protected void Stop()
        {
            m_HitBox.Velocity = Vector2.Zero;
        }

        protected void Move(int rotation)
        {
            Transform.Rotation = rotation;
            switch (rotation)
            {
                case 0:
                    m_HitBox.Velocity = new Vector2(0, MoveSpeed);
                    break;
                case 90:
                    m_HitBox.Velocity = new Vector2(-MoveSpeed, 0);
                    break;
                case 270:
                    m_HitBox.Velocity = new Vector2(MoveSpeed, 0);
                    break;
                case 180:
                    m_HitBox.Velocity = new Vector2(0, -MoveSpeed);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public virtual void OnColisionEnter(Actor other)
        {
            if (other as Bullet != null && m_ShieldSprite.Owner == null)
            {
                m_HitPoint--;
                if (m_HitPoint <= 0)
                {
                    Active = false;
                    Respawn();
                }
            }
        }

        public void OnColisionExit(Actor other) { }


        private ObjectPool<Bullet> m_BulletPool;
        private ObjectPool<Explosion> m_ExplosionPool;
        private HitBox m_HitBox;

        private Drawable m_NormalSprite;
        private Drawable m_ShieldSprite;
        private Drawable m_AppearingSprite;

        protected float MoveSpeed { get; set; } = 8;
        private float m_BulletSpeed = 24;

        private bool m_CanShoot = false;
        private int m_HitPoint;
        public Transform RespawnTransform { get; set; }
        private const double APPEAR_TIME = 2;
        private const double SHIELD_DURATION = 3.5;
        private double m_ShootCooldown = 0.6;
    }
}

