using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BattleCity.Core
{
    public class Drawable : Component
    {


        public Drawable(string textureName, Rectangle rect, int drawOrder, uint pixelPerUnit, bool visible = true)
        {
            DrawOrder = drawOrder;
            Visible = visible;
            PixelPerUnit = pixelPerUnit;
            TextureName = textureName;
            Rect = rect;
        }

        public int DrawOrder { get; protected set; }
        public bool Visible { get; protected set; }
        public uint PixelPerUnit { get; protected set; }
        public Color DrawColor { get; set; } = Color.White;
        public Rectangle Rect { get; set; }
        public string TextureName { get; private set; }


        protected override void OnAttach()
        {

        }

        protected override void OnDeattach()
        {

        }

        protected override void OnDisable()
        {
            DrawWorld.Remove(this);
        }

        protected override void OnEnable()
        {
            DrawWorld.Add(this);
        }
        
        static DrawWorld DrawWorld = TickManager.FindAs<DrawWorld>();
    }
}
