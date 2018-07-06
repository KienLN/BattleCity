using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Core
{
    public abstract class Component
    {
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void OnAttach() { }
        protected virtual void OnDeattach() { }

        public void OnActorStateChange(Actor.ActorState actorState)
        {
            switch (actorState)
            {
                case Actor.ActorState.Active:
                    OnEnable();
                    break;
                case Actor.ActorState.Inactive:
                    OnDisable();
                    break;
                case Actor.ActorState.Destroy:
                    Owner = null;
                    break;
                default:
                    break;
            }
        }

        public Actor Owner
        {
            get { return m_Owner; }
            set
            {
                var newOwner = value;
                var currentOwner = m_Owner;
                if (newOwner != currentOwner)
                {
                    if (currentOwner != null)
                    {
                        currentOwner.OnStateChange -= OnActorStateChange;
                        if (currentOwner.Active) OnDisable();
                        OnDeattach();
                        m_Owner = null;
                    }

                    if (newOwner != null)
                    {
                        newOwner.OnStateChange += OnActorStateChange;
                        m_Owner = newOwner;
                        OnAttach();
                        if (m_Owner.Active) OnEnable();
                    }
                }
            }
        }


        private Actor m_Owner;
    }

}
