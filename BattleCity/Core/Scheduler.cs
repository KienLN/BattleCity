using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Core
{
    public class Scheduler : ITickObject
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
        void ITickObject.OnInit()
        {
        }
        void ITickObject.OnStart()
        {
        }
        #endregion

        class TaskInfo
        {
            public TaskInfo(Action task, double delay, bool repeated)
            {
                Task = task;
                Delay = delay;
                Repeated = repeated;
                Elapsed = 0;
            }

            internal readonly Action Task;
            internal readonly double Delay;
            internal readonly bool Repeated;
            internal double Elapsed;
        }


        void ITickObject.OnTick()
        {
            foreach (var info in m_TaskList.ToArray())
            {
                info.Elapsed += Application.FrameRate;
                if (info.Elapsed >= info.Delay)
                {
                    info.Task?.Invoke();
                    info.Elapsed = 0;
                    if (!info.Repeated) m_TaskList.Remove(info);
                }
            }
        }

        public void Add(Action task, double delay, bool repeated)
        {
            m_TaskList.Add(new TaskInfo(task, delay, repeated));
        }

        public void RemoveAllByTaskOwner(object owner)
        {
            foreach (var info in m_TaskList.ToArray())
            {
                if (info.Task.Target == owner) m_TaskList.Remove(info);
            }
        }

        List<TaskInfo> m_TaskList = new List<TaskInfo>();
    }
}
