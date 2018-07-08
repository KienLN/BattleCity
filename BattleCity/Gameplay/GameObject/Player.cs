using BattleCity.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BattleCity.Gameplay.GameObject
{
    public class Player : Tank
    {
        protected override void OnTick()
        {
            if (Application.Input.IsKeyDown(Keys.A))
            {
                Move(90);
            }
            else if (Application.Input.IsKeyDown(Keys.D))
            {
                Move(270);
            }
            else if (Application.Input.IsKeyDown(Keys.W))
            {
                Move(0);
            }
            else if (Application.Input.IsKeyDown(Keys.S))
            {
                Move(180);
            }
            else
            {
                Stop();
            }

            if (Application.Input.IsKeyPressed(Keys.Space))
            {
                Shoot();
            }
        }

        protected override void OnRespawn(ref Drawable normalSprite, ref float bulletSpeed, ref double shootCooldown, ref int hitPoint)
        {
            normalSprite = Application.ContentLoader.CreateDrawable("Player1");
            MoveSpeed *= 1.25f;
            bulletSpeed = 25;
            shootCooldown = 0.2;
            hitPoint = 1;
        }
    }
}
