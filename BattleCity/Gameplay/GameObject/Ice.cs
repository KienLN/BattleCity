using BattleCity.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Gameplay.GameObject
{
    class Ice : Actor
    {
        public Ice()
        {
            new HitBox(Vector2.One, true, true).Owner = this;
            Application.ContentLoader.CreateDrawable("IceWall", -10).Owner = this;
        }
    }
}
