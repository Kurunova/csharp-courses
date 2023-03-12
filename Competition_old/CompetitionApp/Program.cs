using System.Collections;
using Competition;

class Program
{
	public static void Main(string[] args)
	{
		var results = new List<string>();
		
		var dict = new List<string>();
		int inputCount = Convert.ToInt32(Console.ReadLine().Trim());
		for (int i = 0; i < inputCount; i++)
		{
			var data = Console.ReadLine().TrimEnd();
			dict.Add(data);
		}
		
		int count = Convert.ToInt32(Console.ReadLine().Trim());
		for (int j = 0; j < count; j++)
		{
			var data = Console.ReadLine().TrimEnd();
			var result = JSolution.Execute(data, dict);
			results.Add(result);
		}

		Solution.PrintResults(results);
	}
}