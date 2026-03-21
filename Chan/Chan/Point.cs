namespace Chan;

public class Point : IComparable<Point>
{
    public int X, Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public int CompareTo(Point other)
    {
        return Y != other.Y ? Y.CompareTo(other.Y) : X.CompareTo(other.X);
    }
}