using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    public static class TickManager
    {
        class TickInfo
        {
            public bool Initialized { get; set; } = false;
            public bool Active { get; set; } = true;
            public bool Started { get; set; } = false;
            public bool LockEnable { get; set; } = false;
        }

        public static void Update()
        {


            if (m_ModifyTickListFlag)
            {
                m_ModifyTickListFlag = false;
                m_Temp = m_TickList.ToList();
            }



            foreach (var item in m_Temp)
            {
                if (item.Value.Active) item.Key.OnTick();
            }



            if (m_KillList.Count > 0)
            {
                foreach (var tickObject in m_KillList)
                {
                    tickObject.OnDestroy();
                }
                foreach (var tickObject in m_KillList)
                {
                    m_Map.Remove(tickObject);
                }
                m_KillList.Clear();
            }
        }


        public static T Create<T>() where T : ITickObject, new()
        {
            T tickObject = new T();
            m_Map.Add(tickObject, new TickInfo());

            tickObject.OnInit();
            m_Map[tickObject].Initialized = true;

            return tickObject;
        }


        public static T Spawn<T>() where T : ITickObject, new()
        {
            T tickObject = Create<T>();

            if (m_Map[tickObject].Active)
            {
                m_Map[tickObject].Active = false;
                SetActive(tickObject, true);
            }

            return tickObject;
        }

        // Không thể Destroy tick object trong Constructor của nó.
        public static void Destroy(ITickObject tickObject)
        {
            Debug.Assert(m_Map.ContainsKey(tickObject), "Cannot call Destroy() tick object in its constructor.");
            m_KillList.Add(tickObject);
            SetActive(tickObject, false);
        }

        // Không thể gọi trong Constructor của tick object.
        public static void DestroyAllByType<T>() where T : class, ITickObject
        {
            foreach (var mapItem in m_Map)
            {
                var tmp = mapItem.Key as T;
                if (tmp != null) Destroy(mapItem.Key);
            }
        }

        public static T FindAs<T>() where T : class, ITickObject
        {
            foreach (var mapItem in m_Map)
            {
                var tmp = mapItem.Key as T;
                if (tmp != null) return tmp;
            }
            return null;
        }

        public static List<T> FindAllByType<T>() where T : class, ITickObject
        {
            var ret = new List<T>();
            foreach (var mapItem in m_Map)
            {
                var tmp = mapItem.Key as T;
                if (tmp != null) ret.Add(tmp);
            }
            return ret;
        }


        // Nếu tick object đã được initialize thì sẽ gọi các hàm Enable() và Disable().
        // Không thể gọi trong Constructor của tick object.
        public static void SetActive(ITickObject tickObject, bool active)
        {
            Debug.Assert(m_Map.ContainsKey(tickObject), "Cannot call SetActive() tick object in its constructor.");
            if (m_Map[tickObject].Initialized)
            {
                if (m_Map[tickObject].Active && !active)
                {
                    m_Map[tickObject].Active = false;

                    m_TickList.Remove(tickObject);
                    m_ModifyTickListFlag = true;

                    tickObject.OnDisable();
                }
                else
                {
                    if (!m_KillList.Contains(tickObject))
                    {
                        m_Map[tickObject].Active = true;

                        m_TickList.Add(tickObject, m_Map[tickObject]);
                        m_ModifyTickListFlag = true;

                        tickObject.OnEnable();
                        if (!m_Map[tickObject].Started)
                        {
                            if (m_Map[tickObject].Active)
                            {
                                m_Map[tickObject].Started = true;
                                tickObject.OnStart();
                            }
                        }
                    }
                }

                {
                    //if (!m_Map[tickObject].PendingKill)
                    //{
                    //    if (!m_Map[tickObject].Active && active)
                    //    {
                    //        m_Map[tickObject].Active = true;

                    //        m_TickList.Add(tickObject, m_Map[tickObject]);
                    //        m_ModifyTickListFlag = true;

                    //        tickObject.OnEnable();
                    //        if (!m_Map[tickObject].Started)
                    //        {
                    //            if (m_Map[tickObject].Active)
                    //            {
                    //                tickObject.OnStart();
                    //                m_Map[tickObject].Started = true;
                    //            }
                    //        }
                    //    }
                    //    else if (m_Map[tickObject].Active && !active)
                    //    {
                    //        m_Map[tickObject].Active = false;

                    //        m_TickList.Remove(tickObject);
                    //        m_ModifyTickListFlag = true;

                    //        tickObject.OnDisable();
                    //    }
                    //}
                    //else
                    //{
                    //    if (m_Map[tickObject].Active && !active)
                    //    {
                    //        m_Map[tickObject].Active = false;

                    //        m_TickList.Remove(tickObject);
                    //        m_ModifyTickListFlag = true;

                    //        tickObject.OnDisable();
                    //    }
                    //}
                }

            }
            else
            {
                if (m_KillList.Contains(tickObject))
                {
                    m_Map[tickObject].Active = false;
                }
                else
                {
                    m_Map[tickObject].Active = active;
                }
            }

        }

        // Không thể gọi trong Constructor của tickobject.
        public static bool IsActive(ITickObject tickObject)
        {
            if (m_Map.ContainsKey(tickObject))
            {
                return m_Map[tickObject].Active;
            }
            return false;
        }

        // Chứa tất cả ITickObject đã tạo ra( từ hàm Create<T>()) mà chưa bị destroy.
        static Dictionary<ITickObject, TickInfo> m_Map = new Dictionary<ITickObject, TickInfo>();

        // Chứa những object đang active và được tick mỗi frame.
        static Dictionary<ITickObject, TickInfo> m_TickList = new Dictionary<ITickObject, TickInfo>();

        // 
        static bool m_ModifyTickListFlag = true;

        //
        static List<KeyValuePair<ITickObject, TickInfo>> m_Temp;


        // Chứa những object sẽ bị destroy.
        static HashSet<ITickObject> m_KillList = new HashSet<ITickObject>();

    }
}


//static List<KeyValuePair<ITickObject, TickInfo>> m_TempMapItemList = new List<KeyValuePair<ITickObject, TickInfo>>();
//static bool m_MapModified = true;
//static public event EventHandler<TickEventArgs> InitializeTickObject;
