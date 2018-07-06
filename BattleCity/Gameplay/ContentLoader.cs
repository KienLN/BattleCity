using BattleCity.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BattleCity.Gameplay
{
    public class ContentLoader
    {
        public ContentLoader()
        {
            var tex = Application.Content.Load<Texture2D>("BattleCityAtlas");
            TextureMap.Add(tex.Name, tex);

            string filePath = @"BattleCityAtlas.atlas";

            string[] lines;

            if (System.IO.File.Exists(filePath))
            {
                lines = System.IO.File.ReadAllLines(filePath);
                RegionCount = (lines.Length - 6.0f) / 7.0f;

                if (RegionCount > 0) // Có ít nhất 1 rectangle trong texture.
                {
                    for (int i = 6; i < lines.Length; i += 7)
                    {
                        int rectX, rectY, sizeX, sizeY;
                        string name = lines[i];

                        var xy = Regex.Matches(lines[i + 2], @"\d+");
                        var size = Regex.Matches(lines[i + 3], @"\d+");

                        rectX = int.Parse(xy[0].ToString());
                        rectY = int.Parse(xy[1].ToString());

                        sizeX = int.Parse(size[0].ToString());
                        sizeY = int.Parse(size[1].ToString());

                        RectMap.Add(name, new Rectangle(rectX, rectY, sizeX, sizeY));
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }


            Map = Application.Content.Load<TiledMap>("Battle_City");
        }

        public Drawable CreateDrawable(string name, int drawOrder = 0, uint pixelPerUnit = 16, bool visible = true)
        {
            return new Drawable("BattleCityAtlas", RectMap[name], drawOrder, pixelPerUnit, visible);
        }

        public AnimatedSprite CreateAnimatedSprite(params string[] names)
        {
            Rectangle[] rectangles = new Rectangle[names.Length];
            for (int i = 0; i < rectangles.Length; i++)
            {
                rectangles[i] = RectMap[names[i]];
            }
            return new AnimatedSprite("BattleCityAtlas", rectangles, 0, 16, true);
        }


        float RegionCount { get; }
        public TiledMap Map { get; }

        public Dictionary<string, Rectangle> RectMap { get; } = new Dictionary<string, Rectangle>();
        public Dictionary<string, Texture2D> TextureMap { get; } = new Dictionary<string, Texture2D>();
    }
}
