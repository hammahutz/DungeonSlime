using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonSlime.Engine.Graphics;

public class Tileset
{
    private readonly TextureRegion[] _tiles;
    public int TileWidth { get; }
    public int TileHeight { get; }
    public int Columns { get; }
    public int Rows { get; }
    public int Count { get; }


    public Tileset(TextureRegion textureRegion, int tileWidth, int tileHeight)
    {
        TileWidth = tileWidth;
        TileHeight = tileHeight;
        Columns = textureRegion.Width / tileWidth;
        Rows = textureRegion.Height / tileHeight;
        Count = Columns * Rows;

        _tiles = new TextureRegion[Count];

        for (int i = 0; i < Count; i++)
        {
            int x = i % Columns * tileWidth;
            int y = i / Columns * tileHeight;
            _tiles[i] = new TextureRegion($"{textureRegion.Name}-{i}", textureRegion.Texture, textureRegion.SourceRectangle.X + x, textureRegion.SourceRectangle.Y + y, tileWidth, tileHeight);
        }
    }

    public TextureRegion GetTile(int index) => _tiles[index];
    public TextureRegion GetTile(int column, int row) => GetTile(row * Columns + column);

    public static Tileset GetTileSetFromXML(ContentManager content, XDocument document)
    {
        XElement root = document.Root;
        XElement tilesetElement = root.Element("Tileset");

        string name = tilesetElement.Attribute("name").Value;
        string regionAttribute = tilesetElement.Attribute("region").Value;
        string[] split = regionAttribute.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        int x = int.Parse(split[0]);
        int y = int.Parse(split[1]);
        int width = int.Parse(split[2]);
        int height = int.Parse(split[3]);

        int tileWidth = int.Parse(tilesetElement.Attribute("tileWidth").Value);
        int tileHeight = int.Parse(tilesetElement.Attribute("tileHeight").Value);
        string contentPath = tilesetElement.Value;
        Texture2D texture = content.Load<Texture2D>(contentPath);
        TextureRegion textureRegion = new TextureRegion(name, texture, x, y, width, height);

        return new Tileset(textureRegion, tileWidth, tileHeight);
    }

}