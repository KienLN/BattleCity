using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    interface ITickEventHandler
    {
        void InitializeTickObject(object sender,TickEventArgs tickEventArgs);
        void BeginTickObject(object sender, TickEventArgs tickEventArgs);
        void EndTickObject(object sender, TickEventArgs tickEventArgs);
        void DestroyTickObject(object sender, TickEventArgs tickEventArgs);
    }
}
