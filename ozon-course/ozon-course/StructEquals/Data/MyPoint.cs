namespace ozon_course.StructEquals.Data;

public class MyPoint : IEquatable<MyPoint>
{
	public readonly int x;
	public readonly int y;
	public readonly int z;

	public MyPoint(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public bool Equals(MyPoint? other)
	{
		return x == other.x && y == other.y && z == other.z;
	}
	
	public override bool Equals(object? obj)
	{
		return obj is MyPoint other && Equals(other);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(x, y, z);
	}

	public static bool operator ==(MyPoint left, MyPoint right)
	{
		return left.Equals(right);
	}
	
	public static bool operator !=(MyPoint left, MyPoint right)
	{
		return !left.Equals(right);
	}
}