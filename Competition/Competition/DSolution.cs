class Program
{
	public static void Main(string[] args)
	{
		var results = new List<string>();
		
		int generalCount = Convert.ToInt32(Console.ReadLine().Trim());

		for (int g = 0; g < generalCount; g++)
		{
			int inputCount = Convert.ToInt32(Console.ReadLine().Trim());

			var data = Console.ReadLine().TrimEnd().Split(' ').Select(arTemp => Convert.ToInt32(arTemp)).ToList();
			var result = Execute(data);
			results.Add(result);
		}
		
		PrintResults(results);
	}

	private static string Execute(List<int> data)
	{
		var sorted = data.Select((value, index) => new { Index = index, Value = value })
			.OrderBy(p => p.Value)
			.ToList();
		
		var results = new int[sorted.Count];

		//var previousValue = sorted[0].Value;
		int previousPlaceCounter = 1;
		
		for (int i = 0; i < sorted.Count; i++)
		{
			if (i == 0)
			{
				results[sorted[i].Index] = previousPlaceCounter;
				//previousValue = sorted[i].Value;
				continue;
			}

			if (sorted[i].Value - sorted[i-1].Value <= 1)
			{
				results[sorted[i].Index] = previousPlaceCounter;

				continue;
			}

			previousPlaceCounter = i + 1;
			results[sorted[i].Index] = previousPlaceCounter;
			//previousValue = sorted[i].Value;
		}
		
		return string.Join(" ", results);
	}
	
	private static void PrintResults(List<string> results)
	{		
		foreach (var result in results)
		{
			Console.WriteLine(result);
		}
	}
}