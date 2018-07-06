using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Core
{
    public abstract class Scene : ITickObject
    {
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
            OnLoad();
        }
        void ITickObject.OnTick()
        {

        }

        protected abstract void OnLoad();
    }
}
