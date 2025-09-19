using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonSlime.Engine.Graphics;

public class TileMap
{
    private readonly Tileset _tileSet;
    private readonly int[] _tiles;

    public int Rows { get; }
    public int Columns { get; }
    public int Count { get; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float TileWidth => _tileSet.TileWidth * Scale.X;
    public float TileHeight => _tileSet.TileHeight * Scale.Y;

    public TileMap(Tileset tileSet, int columns, int rows)
    {
        _tileSet = tileSet;
        Rows = rows;
        Columns = columns;
        Count = Columns * Rows;
        _tiles = new int[Count];
    }

    public void SetTile(int index, int tileSetId) => _tiles[index] = tileSetId;
    public void SetTile(int column, int row, int tileSetId) => SetTile(row * Columns + column, tileSetId);
    public TextureRegion GetTile(int index) => _tileSet.GetTile(_tiles[index]);
    public TextureRegion GetTile(int column, int row) => GetTile(row * Columns + column);


    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Count; i++)
        {
            int tilesetIndex = _tiles[i];
            TextureRegion tile = _tileSet.GetTile(tilesetIndex);

            int x = i % Columns;
            int y = i / Columns;

            Vector2 position = new Vector2(x * TileWidth, y * TileHeight);
            tile.Draw(spriteBatch, position, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 1.0f);
        }
    }
    public static Tileset FromFile(ContentManager content, params string[] relativeFilePath)
    {
        string filePath = Path.Combine([content.RootDirectory, .. relativeFilePath]);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument document = XDocument.Load(reader);
                XElement root = document.Root;
                XElement tilesetElement = root.Element("Tileset");


                Tileset tileset = GetTileSet(content, tilesetElement);

            }
        }

        return null;
    }

    private static Tileset GetTileSet(ContentManager content, XElement tilesetElement)
    {
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
        TextureRegion textureRegion = new TextureRegion(texture, x, y, width, height);

        return new Tileset(textureRegion, tileWidth, tileHeight);
    }
}