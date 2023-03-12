// class Program
// {
// 	public static void Main(string[] args)
// 	{
// 		var results = new List<string>();
// 		
// 		int inputCount = Convert.ToInt32(Console.ReadLine().Trim());
// 		for (int i = 0; i < inputCount; i++)
// 		{
// 			var data = Console.ReadLine().TrimEnd().Split(' ').Select(arTemp => Convert.ToInt32(arTemp)).ToList();
// 			var result = Execute(data);
// 			results.Add(result);
// 		}
//
// 		PrintResults(results);
// 	}
//
// 	private static string Execute(List<int> data)
// 	{
// 		var rule = new Dictionary<int, int>()
// 		{
// 			{ 1, 4 },
// 			{ 2, 3 },
// 			{ 3, 2 },
// 			{ 4, 1 }
// 		};
//
// 		var results = new int[4];
// 		
// 		foreach (var item in data)
// 		{
// 			results[item - 1]++;
// 		}
//
// 		bool isValid = true;
//
// 		for (int i = 0; i < results.Length; i++)
// 		{
// 			if (results[i] == rule[i + 1])
// 				continue;
//
// 			isValid = false;
// 			break;
// 		}
// 		
// 		return isValid ? "YES" : "NO";
// 	}
// 	
// 	private static void PrintResults(List<string> results)
// 	{		
// 		foreach (var result in results)
// 		{
// 			Console.WriteLine(result);
// 		}
// 	}
// }