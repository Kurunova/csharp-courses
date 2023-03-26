using BenchmarkDotNet.Attributes;
using ozon_course.StructEquals.Data;

namespace ozon_course.StructEquals;

[MemoryDiagnoser()]
internal class StructEqualsTest
{
	[Benchmark]
	public void TestStruct()
	{
		var first = new MyPoint(1, 2, 3);
		var second = new MyPoint(1, 2, 3);

		var set = new HashSet<MyPoint>();

		for (int i = 0; i < 100; i++)
		{
			set.Add(first);
			set.Add(second);
		}
	}
	
	[Benchmark]
	public void TestStructOverride()
	{
		var first = new MyPointOverride(1, 2, 3);
		var second = new MyPointOverride(1, 2, 3);

		var set = new HashSet<MyPointOverride>();

		for (int i = 0; i < 100; i++)
		{
			set.Add(first);
			set.Add(second);
		}
	}
	
	[Benchmark]
	public void TestStructDefault()
	{
		var first = new MyPointDefault(1, 2, 3);
		var second = new MyPointDefault(1, 2, 3);

		var set = new HashSet<MyPointDefault>();

		for (int i = 0; i < 100; i++)
		{
			set.Add(first);
			set.Add(second);
		}
	}
}