using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleCity.Core;

namespace BattleCity.Gameplay.GameObject
{
    class Enemy : Tank
    {
        protected override void OnRespawn(ref Drawable normalSprite, ref float bulletSpeed, ref double shootCooldown, ref int hitPoint)
        {
            ChangeDirTime = 0.4;

            int type = rand.Next(1, 5);
            switch (type)
            {
                case 1:
                    SetAsBasicTank(ref normalSprite, ref bulletSpeed, ref shootCooldown, ref hitPoint);
                    break;
                case 2:
                    SetAsFastTank(ref normalSprite, ref bulletSpeed, ref shootCooldown, ref hitPoint);
                    break;
                case 3:
                    SetAsPowerTank(ref normalSprite, ref bulletSpeed, ref shootCooldown, ref hitPoint);
                    break;
                case 4:
                    SetAsArmorTank(ref normalSprite, ref bulletSpeed, ref shootCooldown, ref hitPoint);
                    break;
                default:
                    break;
            }
        }

        void SetAsBasicTank(ref Drawable normalSprite, ref float bulletSpeed, ref double shootCooldown, ref int hitPoint)
        {
            hitPoint = 1;
            shootCooldown *= 2;
            switch (rand.Next(1, 3))
            {
                case 1:
                {
                    MoveSpeed *= 0.8f;
                    normalSprite = Application.ContentLoader.CreateDrawable("BasicTank");
                    break;
                }
                case 2:
                {
                    normalSprite = Application.ContentLoader.CreateDrawable("BasicTankRed");
                    break;
                }
                default:
                    break;
            }
        }

        void SetAsFastTank(ref Drawable normalSprite, ref float bulletSpeed, ref double shootCooldown, ref int hitPoint)
        {
            hitPoint = 1;
            bulletSpeed *= 1.8f;
            shootCooldown *= 1.5f;

            switch (rand.Next(1, 3))
            {
                case 1:
                {
                    MoveSpeed *= 3;
                    normalSprite = Application.ContentLoader.CreateDrawable("FastTank");
                    break;
                }
                case 2:
                {
                    MoveSpeed *= 12;
                    shootCooldown *= 1.5f;
                    bulletSpeed *= 4.5f;
                    normalSprite = Application.ContentLoader.CreateAnimatedSprite("FastTank", "FastTankRed");
                    break;
                }
                default:
                    break;
            }
        }

        void SetAsPowerTank(ref Drawable normalSprite, ref float bulletSpeed, ref double shootCooldown, ref int hitPoint)
        {
            hitPoint = 2;
            switch (rand.Next(1, 3))
            {
                case 1:
                {
                    shootCooldown *= 0.75f;
                    bulletSpeed *= 2.25f;
                    normalSprite = Application.ContentLoader.CreateDrawable("PowerTank");
                    break;
                }
                case 2:
                {
                    shootCooldown *= 0.5f;
                    bulletSpeed *= 3.5f;
                    normalSprite = Application.ContentLoader.CreateAnimatedSprite("PowerTank", "PowerTankRed");
                    break;
                }
                default:
                    break;
            }
        }

        void SetAsArmorTank(ref Drawable normalSprite, ref float bulletSpeed, ref double shootCooldown, ref int hitPoint)
        {
            normalSprite = Application.ContentLoader.CreateAnimatedSprite("ArmorTank",
                "ArmorTankBrown", "ArmorTankDarkGreen", "ArmorTankHunter", "ArmorTankRed");

            hitPoint = 4;
            bulletSpeed *= 0.8f;
        }

        protected override void OnTick()
        {
            base.OnTick();
            Shoot();
            if ((MoveTime += Application.FrameRate) >= ChangeDirTime)
            {
                MoveTime = 0;
                ChangeDirTime = rand.NextDouble() * 0.6;
                switch (rand.Next(1, 5))
                {
                    case 1:
                        Move(0);
                        break;
                    case 2:
                        Move(90);
                        break;
                    case 3:
                        Move(270);
                        break;
                    case 4:
                        Move(180);
                        break;
                    default:
                        break;
                }
            }
        }

        static Random rand = new Random();

        double ChangeDirTime = 0.4f;
        double MoveTime = 0;
    }
}
