using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonSlime.Library.Graphics;

public class Sprite
{
    public TextureRegion Region { get; set; }
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0.0f;
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; }
    public float Width => Region.Width * Scale.X;
    public float Height => Region.Height * Scale.Y;

    public Sprite() { }

    public Sprite(TextureRegion region)
    {
        Region = region;
        CenterOrigin();
    }

    public void CenterOrigin() => Origin = new Vector2(Region.Width, Region.Height) * 0.5f;

    public void Draw(SpriteBatch spriteBatch, Vector2 position) =>
        Region.Draw(spriteBatch, position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
}
