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



            Actor.Spawn("Player", new Vector2(16, 16));

            LoadMapFromTileMap();

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
                        var position = new Vector2(x + 0.25f, y - 0.25f);

                        (Actor.Spawn("BrickWall", position) as BrickWall).SetBrickType(BrickWall.Type.TopLeft);
                        (Actor.Spawn("BrickWall", position + Vector2.UnitX * 0.5f) as BrickWall).SetBrickType(BrickWall.Type.TopRight);
                        (Actor.Spawn("BrickWall", position - Vector2.UnitY * 0.5f) as BrickWall).SetBrickType(BrickWall.Type.BottomLeft);
                        (Actor.Spawn("BrickWall", position + new Vector2(0.5f, -0.5f)) as BrickWall).SetBrickType(BrickWall.Type.BottomRight);
                    }
                    continue;
                }
                else
                {
                    foreach (var mapObject in objectLayer.Objects)
                    {
                        var x = mapObject.Position.X / 16;
                        var y = Application.ViewportAdapter.VirtualHeight - mapObject.Position.Y / 16;
                        var position = new Vector2(x + 0.5f * mapObject.Size.Width / 16, y - 0.5f * mapObject.Size.Height / 16);

                        Actor.Spawn(objectLayer.Name, position);
                    }
                }
            }
        }
    }
}
