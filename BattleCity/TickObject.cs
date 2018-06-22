using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    public abstract class TickObject
    {
        private enum Flags : UInt32
        {
            None = 1 << 0x00,
            Alive = 1 << 0x01,
            Active = 1 << 0x02,
            Initialized = 1 << 0x03,
        }

        #region Constructor
        protected TickObject()
        {
            m_flags = (UInt32)(Flags.Alive | Flags.Active);
            m_tickRate = 0;
            CurrentStateFunc = Init;
            TickLayer = 0;
        }
        #endregion


        #region State
        // Từ Init chỉ chuyển được sang BeginTick hoặc null.
        private void Init()
        {
            OnInit();
            if ((m_flags & (UInt32)Flags.Active) > 0)
            {
                CurrentStateFunc = BeginTick;
                TickManager.Get().Add(this);
            }
            else
            {
                CurrentStateFunc = null;
            }
            m_flags |= (UInt32)Flags.Initialized;
        }

        // Từ BeginTick chỉ chuyển được sang EndTick hoặc Tick.
        private void BeginTick()
        {
            OnBeginTick();
            if ((m_flags & (UInt32)Flags.Active) > 0)
            {
                CurrentStateFunc = Tick;
            }
            else
            {
                CurrentStateFunc = EndTick;
            }
        }

        // Từ Tick chỉ chuyển được sang EndTick.
        private void Tick()
        {
            OnTick(m_tickRate);
            if ((m_flags & (UInt32)Flags.Active) == 0)
            {
                CurrentStateFunc = EndTick;
            }
        }

        // Từ EndTick chỉ chuyển được sang BeginTick hoặc null.
        private void EndTick()
        {
            OnEndTick();
            if ((m_flags & (UInt32)Flags.Active) > 0)
            {
                CurrentStateFunc = BeginTick;
            }
            else
            {
                CurrentStateFunc = null;
                TickManager.Get().Remove(this);
            }
        }
        #endregion


        #region PublicMethods
        // Không được gọi hàm này từ lớp kế thừa.
        public void Update(double tickRate)
        {
            m_tickRate = tickRate;
            CurrentStateFunc();
            //CurrentStateFunc?.Invoke();
        }

        public void SetActive(bool active)
        {
            if (active)
            {
                if (!IsActive())
                {
                    TickManager.Get().Add(this);
                    CurrentStateFunc = BeginTick;
                }
                m_flags |= (UInt32)Flags.Active;
            }
            else
            {
                m_flags &= (UInt32)~Flags.Active;
            }
        }

        // Inactive là khi CurrentStateFunc không chạy bất kể phương thức nào.
        public bool IsActive()
        {
            return CurrentStateFunc != null;
        }

        public bool IsInitialized()
        {
            return (m_flags & (UInt32)Flags.Initialized) > 0;
        }

        public static T Create<T>() where T : TickObject, new()
        {
            return TickManager.Get().Add<T>();
        }

        #endregion


        #region Fields
        private delegate void UpdateStateDelegate();
        private UpdateStateDelegate CurrentStateFunc;
        private UInt32 m_flags;

        private double m_tickRate;
        public readonly int TickLayer;
        #endregion


        #region VirtualMethods
        protected virtual void OnInit() { }
        protected virtual void OnBeginTick() { }
        protected virtual void OnTick(double tickRate) { }
        protected virtual void OnEndTick() { }
        #endregion
    }
}
