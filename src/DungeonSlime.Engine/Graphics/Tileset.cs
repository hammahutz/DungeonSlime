using System.Dynamic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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
            _tiles[i] = new TextureRegion(textureRegion.Texture, textureRegion.SourceRectangle.X + x, textureRegion.SourceRectangle.Y + y, tileWidth, tileHeight);
        }
    }

    public TextureRegion GetTile(int index) => _tiles[index];
    public TextureRegion GetTile(int column, int row) => GetTile(row * Columns + column);


}