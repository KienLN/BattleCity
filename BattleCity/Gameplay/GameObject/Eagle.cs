using BattleCity.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Gameplay.GameObject
{
    class Eagle : Actor
    {
        public Eagle()
        {
            new HitBox(Vector2.One, true, true).Owner = this;
            Application.ContentLoader.CreateDrawable("EagleNormal").Owner = this;
        }
    }
}
