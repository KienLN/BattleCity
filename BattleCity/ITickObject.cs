using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    public interface ITickObject
    {
        void OnInit();
        void OnDestroy();

        void OnStart();

        void OnEnable();
        void OnDisable();

        void OnTick();
    }
}
