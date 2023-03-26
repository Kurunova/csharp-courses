// using BenchmarkDotNet.Attributes;
// using ozon_course.StructExample.Data;
//
// namespace ozon_course.StructExample;
//
// [MemoryDiagnoser()]
// public class StructExampleTest
// {
// 	[Benchmark]
// 	public int TestStruct()
// 	{
// 		var user = new User2();
// 		
// 		var res = 0;
//
// 		for (int i = 0; i < 100; i++)
// 		{
// 			if (CanReadyForCredit(ref user.Salary))
// 			{
// 				res++;
// 			}
// 		}
//
// 		return res;
// 	}
//
// 	private bool CanReadyForCredit(ref SalaryStruct userSalary)
// 	{
// 		return userSalary.AvgSalary > 1;
// 	}
// 	
// 	[Benchmark]
// 	public int TestStruct2()
// 	{
// 		var user = new User2();
// 		
// 		var res = 0;
//
// 		for (int i = 0; i < 100; i++)
// 		{
// 			if (CanReadyForCredit(user.Salary))
// 			{
// 				res++;
// 			}
// 		}
//
// 		return res;
// 	}
//
// 	private bool CanReadyForCredit(SalaryStruct userSalary)
// 	{
// 		return userSalary.AvgSalary > 1;
// 	}
// }