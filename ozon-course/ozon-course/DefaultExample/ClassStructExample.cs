using BenchmarkDotNet.Attributes;

namespace ozon_course.DefaultExample
{
	public struct MyStruct
	{
		public int Value;
	}
	
	public class MyClass
	{
		public int Value;
	}
	
	internal class ClasStructExample
	{
		[Benchmark]
		[Arguments(10)]
		public void CalcStruct(int val)
		{
			var array = new MyStruct[val];

			for (int i = 0; i < array.Length; i++)
			{
				var st = new MyStruct();
				PrivateCalcStruct(ref st);
				array[i] = st;
			}
		}

		private void PrivateCalcStruct(ref MyStruct st)
		{
			st.Value = 5;
		}
		
		[Benchmark]
		[Arguments(10)]
		public void CalcClass(int val)
		{
			var array = new MyClass[val];

			for (int i = 0; i < array.Length; i++)
			{
				var st = new MyClass();
				PrivateCalcClass(st);
				array[i] = st;
			}
		}

		private void PrivateCalcClass(MyClass st)
		{
			st.Value = 5;
		}
	}
}