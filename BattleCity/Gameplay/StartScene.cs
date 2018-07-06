using BattleCity.Core;
using BattleCity.Gameplay.GameObject;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using System;

namespace BattleCity.Gameplay
{
    class StartScene : Scene
    {
        protected override void OnLoad()
        {
            Application.Input = TickManager.Spawn<Input>();
            Application.ContentLoader = new ContentLoader();
            TickManager.Spawn<WorldSimulator>();
            TickManager.Spawn<DrawWorld>();
            TickManager.Spawn<AnimateWorld>();


            LoadMapFromTileMap();


            Actor.Spawn("Player", new Vector2(16, 16));

        }


        public void LoadMapFromTileMap()
        {
            foreach (var objectLayer in Application.ContentLoader.Map.ObjectLayers)
            {
                if (objectLayer.Name.Equals("BrickWall"))
                {
                    foreach (var mapObject in objectLayer.Objects)
                    {
                        var x = mapObject.Position.X / 16;
                        var y = Application.ViewportAdapter.VirtualHeight - mapObject.Position.Y / 16;
                        var position = new Vector2(x, y);

                        (Actor.Spawn("BrickWall", position) as BrickWall).SetBrickType(BrickWall.Type.TopLeft);
                        (Actor.Spawn("BrickWall", position + Vector2.UnitX * 0.5f) as BrickWall).SetBrickType(BrickWall.Type.TopRight);
                        (Actor.Spawn("BrickWall", position - Vector2.UnitY * 0.5f) as BrickWall).SetBrickType(BrickWall.Type.BottomLeft);
                        (Actor.Spawn("BrickWall", position + new Vector2(0.5f, -0.5f)) as BrickWall).SetBrickType(BrickWall.Type.BottomRight);
                    }
                    continue;
                }



            }
        }


    }
}
