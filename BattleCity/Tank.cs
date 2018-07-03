using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    class Tank : ITickObject
    {
        public Tank()
        {
            Console.WriteLine("Run Constructor.");

        }

        public void OnTick()
        {
            count++;
            Console.WriteLine(count);
            if (count >= 100)
            {
                
            }
        }

        public void OnInit()
        {
            Console.WriteLine("Run OnInit.");

        }

        public void OnDestroy()
        {
            Console.WriteLine("Run OnDestroy.");

        }

        public void OnStart()
        {
            Console.WriteLine("Run OnStart.");

        }

        public void OnEnable()
        {
            Console.WriteLine("Run OnEnable.");

        }

        public void OnDisable()
        {
            Console.WriteLine("Run OnDisable.");

        }



        int count = 0;
    }
}
