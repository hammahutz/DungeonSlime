using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonSlime.Library.Graphics;

public class TextureRegion
{
    public Texture2D Texture { get; set; }
    public Rectangle SourceRectangle { get; set; }
    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;

    public TextureRegion(Texture2D texture, int x, int y, int width, int height)
    {
        Texture = texture;
        SourceRectangle = new Rectangle(x, y, width, height);
    }
}
