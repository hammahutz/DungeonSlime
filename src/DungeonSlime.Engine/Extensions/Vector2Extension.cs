using Microsoft.Xna.Framework;

namespace DungeonSlime.Engine.Extensions;
public static class Vector2Extension
{
    public static Vector2 Add(this Vector2 vector, float value) => new Vector2(vector.X + value, vector.Y + value);
    public static Vector2 Sub(this Vector2 vector, float value) => new Vector2(vector.X - value, vector.Y - value);
    public static Vector2 Mod(this Vector2 vector, float value) => new Vector2(vector.X % value, vector.Y % value);

}