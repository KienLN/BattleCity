using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Core
{
    public class ActorStateChangeEventArgs : EventArgs
    {
        public enum ActorState
        {
            Active, Inactive, Destroy
        }

        public ActorStateChangeEventArgs(ActorState state)
        {
            State = state;
        }

        public ActorState State { get; }
    }
}
