using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    public sealed class TickEventArgs : EventArgs
    {
        public TickEventArgs(ITickObject tickObject)
        {
            TickObject = tickObject;
        }

        public ITickObject TickObject { get; }
    }
}
