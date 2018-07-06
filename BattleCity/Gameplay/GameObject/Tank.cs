using BattleCity.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Gameplay.GameObject
{
    public class Tank : Actor
    {
        public Tank()
        {
            Console.WriteLine("Run Constructor.");

            //m_Drawable = Application.ContentLoader.CreateDrawable("Player2");
            //m_Drawable.Owner = this;

            m_Sprite = Application.ContentLoader.CreateAnimatedSprite("Player1", "Player2", "Player3");
            m_Sprite.Owner = this;

            m_HitBox = new HitBox(new Vector2(1.75f), false, false);
            m_HitBox.Owner = this;

            m_ExplosionPool = new ObjectPool<Explosion>(() =>
            {
                Explosion explosion = Spawn<Explosion>(new Vector2(10, 10));
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

        //protected override void OnTick()
        //{

        //}

        //protected override void OnInit()
        //{
        //    Console.WriteLine("Run OnInit.");

        //}

        //protected override void OnDestroy()
        //{
        //    Console.WriteLine("Run OnDestroy.");

        //}

        //protected override void OnStart()
        //{
        //    Console.WriteLine("Run OnStart.");


        //}

        //protected override void OnEnable()
        //{
        //    Console.WriteLine("Run OnEnable.");
        //}

        //protected override void OnDisable()
        //{
        //    Console.WriteLine("Run OnDisable.");

        //}

        protected void Fire()
        {
            var bullet = m_BulletPool.GetObject();
            bullet.Active = true;
            Bullet.Fire(bullet, Transform);
        }

        private ObjectPool<Bullet> m_BulletPool;
        private ObjectPool<Explosion> m_ExplosionPool;
        protected HitBox m_HitBox;
        protected Drawable m_Drawable;
        protected AnimatedSprite m_Sprite;
    }
}

