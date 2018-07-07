using BattleCity.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Gameplay.GameObject
{
    class Tree : Actor
    {
        public Tree()
        {
            Application.ContentLoader.CreateDrawable("Tree", 10).Owner = this;
        }
    }
}
