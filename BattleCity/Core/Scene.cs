using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Core
{
    public abstract class Scene : ITickObject
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
        void ITickObject.OnTick()
        {

        }
        #endregion

        void ITickObject.OnStart()
        {
            OnLoad();
        }
        protected abstract void OnLoad();
    }
}
