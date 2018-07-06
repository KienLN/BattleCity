using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Core
{
    public interface ICollisionHandler
    {
        void OnColisionEnter(Actor other);
        void OnColisionExit(Actor other);
    }
}
