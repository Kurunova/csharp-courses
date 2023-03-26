namespace ozon_course.StructEquals.Data;

public class MyPointOverride 
{
	public readonly int x;
	public readonly int y;
	public readonly int z;

	public MyPointOverride(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}


	public override bool Equals(object? obj)
	{
		return obj is MyPointOverride other && x == other.x && y == other.y && z == other.z;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(x, y, z);
	}
}