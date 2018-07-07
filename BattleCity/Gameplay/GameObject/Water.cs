using BattleCity.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Gameplay.GameObject
{
    class Water : Actor
    {
        public Water()
        {
            new HitBox(Vector2.One, true, false).Owner = this;
            Application.ContentLoader.CreateDrawable("Water", -10).Owner = this;
        }
    }
}
