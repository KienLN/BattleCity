using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Collision.Shapes;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;

namespace BattleCity.Core
{
    class WorldSimulator : ITickObject
    {
        #region Emty Implementation
        void ITickObject.OnDestroy()
        {
        }
        void ITickObject.OnDisable()
        {
        }
        void ITickObject.OnEnable()
        {
        }
        void ITickObject.OnStart()
        {
        }
        void ITickObject.OnInit()
        {
        }
        #endregion


        void ITickObject.OnTick()
        {
            //foreach (var mapItem in m_BodyMap)
            //{
            //    if (!mapItem.Key.Static) mapItem.Value.Position = mapItem.Key.Owner.Transform.Position;
            //}



            float timeStep = Math.Min((float)Application.FrameRate, 1f / 50f);
            m_World.Step(timeStep);



            Locking = true;
            foreach (var mapItem in m_DynamicBodyMap)
            {
                mapItem.Key.Owner.Transform.Position = mapItem.Value.Position;
            }
            Locking = false;


        }

        public bool SetVelocity(HitBox hitBox, Vector2 velocity)
        {
            if (m_BodyMap.ContainsKey(hitBox))
            {
                m_BodyMap[hitBox].LinearVelocity = velocity;
                return true;
            }
            return false;
        }

        public Vector2 GetVelocity(HitBox hitBox)
        {
            if (m_BodyMap.ContainsKey(hitBox))
            {
                return m_BodyMap[hitBox].LinearVelocity;
            }
            return Vector2.Zero;
        }

        public void SetPosition(HitBox hitBox, Vector2 position)
        {
            if (!Locking)
                m_BodyMap[hitBox].Position = position;
        }


        public void Add(HitBox hitBox)
        {

            var body = BodyFactory.CreateRectangle(
                m_World,
                hitBox.Size.X,
                hitBox.Size.Y,
                0,
                hitBox.Owner.Transform.Position,
                0,
                hitBox.Static ? BodyType.Static : BodyType.Dynamic,
                hitBox.Owner);


            body.IsSensor = hitBox.Trigger;
            body.FixedRotation = true;
            body.Restitution = 0.05f;
            body.Friction = 0;
            body.SleepingAllowed = true;
            body.LinearVelocity = hitBox.StartVelocity;

            if (hitBox.ColisionHandler != null)
            {
                body.OnCollision += (Fixture A, Fixture B, Contact contact) =>
                {
                    hitBox.ColisionHandler.OnColisionEnter(B.Body.UserData as Actor);
                    //if (body == A.Body)
                    //    hitBox.ColisionHandler.OnColisionEnter(B.Body.UserData as Actor);
                    //else
                    //    hitBox.ColisionHandler.OnColisionEnter(A.Body.UserData as Actor);
                };
                body.OnSeparation += (Fixture A, Fixture B, Contact contact) =>
                {
                    hitBox.ColisionHandler.OnColisionExit(B.Body.UserData as Actor);
                    //if (body == A.Body)
                    //    hitBox.ColisionHandler.OnColisionExit(B.Body.UserData as Actor);
                    //else
                    //    hitBox.ColisionHandler.OnColisionExit(A.Body.UserData as Actor);
                };
            }

            m_BodyMap.Add(hitBox, body);
            if (!hitBox.Static)
                m_DynamicBodyMap.Add(hitBox, body);
        }

        public void Remove(HitBox hitBox)
        {
            m_World.RemoveBody(m_BodyMap[hitBox]);
            m_BodyMap.Remove(hitBox);
            if (!hitBox.Static)
                m_DynamicBodyMap.Remove(hitBox);
        }

        public bool Locking { get; private set; } = false;
        World m_World = new World(Vector2.Zero);
        Dictionary<HitBox, Body> m_BodyMap = new Dictionary<HitBox, Body>();
        Dictionary<HitBox, Body> m_DynamicBodyMap = new Dictionary<HitBox, Body>();
    }
}
