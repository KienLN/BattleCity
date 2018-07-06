using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;
using VelcroPhysics.Collision;
using Microsoft.Xna.Framework;

namespace BattleCity.Core
{
    public class HitBox : Component
    {
        public HitBox(Vector2 size, bool isStatic, bool isTrigger)
        {
            Size = new Vector2(size.X, size.Y);
            Static = isStatic;
            Trigger = isTrigger;
        }

        protected override void OnAttach()
        {
            var collisionHandler = Owner as ICollisionHandler;
            if (collisionHandler != null)
            {
                ColisionHandler = collisionHandler;
            }
        }

        protected override void OnDeattach()
        {
        }

        protected override void OnDisable()
        {
            Owner.Transform.OnChange -= OnTransformChange;
            WorldSimulator.Remove(this);
        }

        private void OnTransformChange()
        {
            WorldSimulator.SetPosition(this, Owner.Transform.Position);
        }

        protected override void OnEnable()
        {
            WorldSimulator.Add(this);
            Owner.Transform.OnChange += OnTransformChange;
        }

        public Vector2 Size { get; private set; }
        public bool Static { get; private set; }
        public bool Trigger { get; private set; }
        public Vector2 StartVelocity { get; private set; } = Vector2.Zero;

        public ICollisionHandler ColisionHandler { get; private set; }

        public Vector2 Velocity
        {
            get { return WorldSimulator.GetVelocity(this); }
            set { if (!WorldSimulator.SetVelocity(this, value)) StartVelocity = value; }
        }

        static WorldSimulator WorldSimulator = TickManager.FindAs<WorldSimulator>();
    }
}
