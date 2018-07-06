using BattleCity.Core;
using Microsoft.Xna.Framework;

namespace BattleCity.Gameplay.GameObject
{
    public class BrickWall : Actor, ICollisionHandler
    {
        public enum Type
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        public BrickWall()
        {
            m_HitBox = new HitBox(new Vector2(0.5f), true, false);
            m_HitBox.Owner = this;
        }

        public void SetBrickType(Type type)
        {
            switch (type)
            {
                case Type.TopLeft:
                    m_Drawable = Application.ContentLoader.CreateDrawable("BrickWallTopLeft");
                    break;
                case Type.TopRight:
                    m_Drawable = Application.ContentLoader.CreateDrawable("BrickWallTopRight");
                    break;
                case Type.BottomLeft:
                    m_Drawable = Application.ContentLoader.CreateDrawable("BrickWallBottomLeft");
                    break;
                case Type.BottomRight:
                    m_Drawable = Application.ContentLoader.CreateDrawable("BrickWallBottomRight");
                    break;
                default:
                    break;
            }

            m_Drawable.Owner = this;
        }

        public void OnColisionEnter(Actor other)
        {
            if (other as Explosion != null)
            {
                Destroy(this);
            }
        }

        public void OnColisionExit(Actor other)
        {
            
        }

        HitBox m_HitBox;
        Drawable m_Drawable;
    }
}
