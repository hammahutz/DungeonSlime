using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonSlime.Engine.Graphics;

public class Tilemap
{
    private readonly Tileset _tileSet;
    private readonly int[] _tiles;

    public int Rows { get; }
    public int Columns { get; }
    public int Count { get; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float TileWidth => _tileSet.TileWidth * Scale.X;
    public float TileHeight => _tileSet.TileHeight * Scale.Y;

    public Tilemap(Tileset tileSet, int columns, int rows)
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

    public static Tilemap FromFile(ContentManager content, params string[] relativeFilePath)
    {
        string filePath = Path.Combine([content.RootDirectory, .. relativeFilePath]);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument document = XDocument.Load(reader);
                Tileset tileset = Tileset.GetTileSetFromXML(content, document);

                XElement root = document.Root;
                XElement tiles = root.Element("Tiles");

                string[] rows = tiles.Value.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                int columnsCount = rows[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
                Tilemap tilemap = new Tilemap(tileset, columnsCount, rows.Length);

                for (int row = 0; row < rows.Length; row++)
                {
                    string[] columns = rows[row].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    for (int column = 0; column < columns.Length; column++)
                    {
                        int tilesetIndex = int.Parse(columns[column]);
                        TextureRegion region = tileset.GetTile(tilesetIndex);
                        tilemap.SetTile(column, row, tilesetIndex);
                    }

                }
                return tilemap;
            }
        }
    }
}