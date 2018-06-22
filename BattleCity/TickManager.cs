using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    public class TickManager : TickObject
    {
        #region OverrideBase

        protected override void OnTick(double tickRate)
        {
            var tmpList = new List<HashSet<TickObject>>(m_tickGroupList);

            for (int i = 0; i < tmpList.Count; i++)
            {
                foreach (TickObject tickObject in tmpList[i])
                {
                    tickObject.Update(tickRate);
                }
            }
        }

        protected override void OnEndTick()
        {

        }
        #endregion


        #region PublicMethods
        public void Add(TickObject tickObject)
        {
            if (m_tickGroupList[tickObject.TickLayer] == null)
            {
                m_tickGroupList[tickObject.TickLayer] = new HashSet<TickObject> { tickObject };
            }
            else
            {
                m_tickGroupList[tickObject.TickLayer].Add(tickObject);
            }
        }

        public T Add<T>() where T : TickObject, new()
        {
            T tickObject = new T();
            if (IsInitialized())
            {
                tickObject.Update(0); // Chạy phương thức Init.
                if (tickObject.IsActive())
                {
                    // Nếu object active thì chạy luôn phương thức BeginTick.
                    tickObject.Update(0);
                }
            }
            return tickObject;
        }

        public void Remove(TickObject tickObject)
        {
            for (var i = 0; i < m_tickGroupList.Count; i++)
            {
                m_tickGroupList[i].RemoveWhere(item => item == tickObject);
            }
        }

        // Remove tất cả object có loại T hoặc subclass của T.
        public void Remove<T>()
        {
            for (int i = 0; i < m_tickGroupList.Count; i++)
            {
                m_tickGroupList[i].RemoveWhere(
                    item =>
                    {
                        return item.GetType().IsSubclassOf(typeof(T)) || item.GetType() == typeof(T);
                    });
            }
        }

        public static TickManager Get() { return instance; }
        #endregion


        #region Fields
        private List<HashSet<TickObject>> m_tickGroupList;
        private static TickManager instance = new TickManager();
        private TickManager()
        {
            m_tickGroupList = new List<HashSet<TickObject>>();
        }
        #endregion
    }
}
