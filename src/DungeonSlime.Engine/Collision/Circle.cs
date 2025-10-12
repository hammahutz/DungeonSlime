using Microsoft.Xna.Framework;

namespace DungeonSlime.Engine.Collision;

public readonly record struct Circle(int X, int Y, int Radius)
{
    public readonly Point Location => new Point(X, Y);
    public readonly int Top => Y - Radius;
    public readonly int Bottom => Y + Radius;
    public readonly int Left => X - Radius;
    public readonly int Right => X + Radius;

    public readonly bool IsEmpty => X == 0 && Y == 0 && Radius == 0;
    private static readonly Circle s_empty = new Circle();
    public static Circle Empty => s_empty;
    public Circle(Point location, int radius) : this(location.X, location.Y, radius) { }

    public bool ClampWithin(Rectangle rectangle) => false;

    public bool Intersects(Circle other)
    {
        int radiusSquared = (this.Radius + other.Radius) * (this.Radius + other.Radius);
        float distanceSquared = Vector2.DistanceSquared(this.Location.ToVector2(), other.Location.ToVector2());
        return distanceSquared < radiusSquared;
    }

    // TODO: Add rectangle intersection check
    // public bool Intersects(Rectangle other)
    // {
    //     return false;
    // }
}

// public readonly struct Circle : IEquatable<Circle>
// {
//     private static readonly Circle s_empty = new Circle();

//     public readonly int X;
//     public readonly int Y;
//     public readonly int Radius;

//     public readonly Point Location => new Point(X, Y);
//     public static Circle Empty => s_empty;
//     public readonly bool IsEmpty => X == 0 && Y == 0 && Radius == 0;

//     public readonly int Top => Y - Radius;
//     public readonly int Bottom => Y + Radius;
//     public readonly int Left => X - Radius;
//     public readonly int Right => X + Radius;

//     public Circle(int x, int y, int radius)
//     {
//         X = x;
//         Y = y;
//         Radius = radius;
//     }
//     public Circle(Point location, int radius)
//     {
//         X = location.X;
//         Y = location.Y;
//         Radius = radius;
//     }

//     public override readonly bool Equals([NotNullWhen(true)] object obj) => obj is Circle other && Equals(other);
//     public readonly bool Equals(Circle other) => X == other.X && Y == other.Y && Radius == other.Radius;
//     public override readonly int GetHashCode() => HashCode.Combine(X, Y, Radius);

//     public static bool operator ==(Circle left, Circle right) => left.Equals(right);

//     public static bool operator !=(Circle left, Circle right) => !(left == right);
// }