using Microsoft.Xna.Framework;
using System;

namespace BattleCity.Core
{
    public class Transform
    {
        public Transform() { Position = Vector2.Zero; Rotation = 0; Scale = 1; }
        public Transform(Vector2 position, float rotation = 0, float scale = 1)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public static Transform Default { get; } = new Transform();

        public void Translate(Vector2 offset)
        {
            Position += offset;
        }
        public void Translate(float offsetX, float offsetY)
        {
            Translate(new Vector2(offsetX, offsetY));
        }
        public void TranslateX(float offsetX)
        {
            Translate(offsetX, 0);
        }
        public void TranslateY(float offsetY)
        {
            Translate(0, offsetY);
        }

        public event Action OnChange;

        public float Rotation { get; set; }
        public float Scale { get; set; }

        public Vector2 Position
        {
            get { return m_Position; }
            set
            {
                if (value != m_Position)
                {
                    m_Position = value;
                    OnChange?.Invoke();
                }
            }
        }

        private Vector2 m_Position;
    }
}
