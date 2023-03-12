// class Program
// {
// 	public static void Main(string[] args)
// 	{
// 		var results = new List<string>();
// 		
//
// 		var field = new List<List<int>>();
// 		
// 		for (int g = 0; g < 8; g++)
// 		{
// 			var line = Console.ReadLine().TrimEnd().ToCharArray().Select(p => p == '*' ? 1 : 0).ToList();
// 			field.Add(line);
// 		}
// 		
// 		var figures = new List<List<List<int>>>();
// 		
// 		int generalCount = Convert.ToInt32(Console.ReadLine().Trim());
// 		for (int g = 0; g < generalCount; g++)
// 		{
// 			var figure = new List<List<int>>();
//
// 			int linesCount = Convert.ToInt32(Console.ReadLine().Trim());
// 			for (int i = 0; i < linesCount; i++)
// 			{
// 				var line = Console.ReadLine().TrimEnd().ToCharArray().Select(p => p == '*' ? 1 : 0).ToList();
// 				figure.Add(line);
// 			}
// 			figures.Add(figure);
// 		}
//
// 		foreach (var figure in figures)
// 		{
// 			var result = Execute(field, figure);
// 			results.Add(result);
// 		}
// 		
// 		PrintResults(results);
// 	}
//
// 	private static string Execute(List<List<int>> field, List<List<int>> figure)
// 	{
// 		var fieldRowCount = field.Count;
// 		var fieldColumnCount = field[0].Count;
// 		
// 		var figureRowCount = figure.Count;
// 		var figureColumnCount = figure[0].Count;
//
// 		var results = new List<(int row, int column)>();
// 		
// 		for (int i = 0; i < fieldRowCount - figureRowCount + 1; i++)
// 		{
// 			for (int j = 0; j < fieldColumnCount - figureColumnCount + 1; j++)
// 			{
// 				bool canPlace = true;
// 				for (int k = 0; k < figureRowCount; k++)
// 				{
// 					for (int l = 0; l < figureColumnCount; l++)
// 					{
// 						if(figure[k][l] == 0)
// 							continue;
// 						
// 						if(field[i + k][j + l] == 0)
// 							continue;
//
// 						canPlace = false;
// 					}
// 				}
//
// 				if (canPlace)
// 				{
// 					results.Add((i, j));
// 				}
// 			}
// 		}
//
// 		var lst = new List<int>();
// 		int min = Int32.MaxValue;
// 		foreach (var result in results)
// 		{
// 			var fieldModified = new List<List<int>>();
// 			field.ForEach((item)=>
// 			{
// 				var line = new List<int>();
// 				line.AddRange(item);
// 				fieldModified.Add(line);
// 			});
//
// 			// fill it
// 			for (int i = 0; i < figureRowCount; i++)
// 			{
// 				for (int j = 0; j < figureColumnCount; j++)
// 				{
// 					if (figure[i][j] == 1)
// 						fieldModified[i + result.row][j + result.column] = figure[i][j];
// 				}
// 			}
//
// 			// count rows and columns
// 			var rowsSum = new int[fieldRowCount];
// 			var columnsSum = new int[fieldColumnCount];
//
// 			for (int i = 0; i < fieldRowCount; i++)
// 			{
// 				for (int j = 0; j < fieldColumnCount; j++)
// 				{
// 					var value = fieldModified[i][j];
// 					
// 					if ((result.row <= i && i < result.row + figureRowCount)
// 					    && (result.column <= j && j < result.column + figureColumnCount))
// 					{
// 						if(fieldModified[i][j] == 0 && figure[i - result.row][j - result.column] == 1)
// 							value = figure[i - result.row][j - result.column];
// 					}
//
// 					rowsSum[i] += value;
// 					columnsSum[j] += value;
// 				}
// 			}
//
// 			// burn it
// 			var rowFullValue = rowsSum.Select((p, index) => new { Index = index, Value = p })
// 				.Where(p => p.Value == fieldRowCount);
// 			var columnFullValue = columnsSum.Select((p, index) => new { Index = index, Value = p })
// 				.Where(p => p.Value == fieldColumnCount);
//
// 			foreach (var rowFull in rowFullValue)
// 			{
// 				for (int j = 0; j < fieldColumnCount; j++)
// 				{
// 					fieldModified[rowFull.Index][j] = 0;
// 				}
// 			}
// 			
// 			foreach (var columnFull in columnFullValue)
// 			{
// 				for (int i = 0; i < fieldRowCount; i++)
// 				{
// 					fieldModified[i][columnFull.Index] = 0;
// 				}
// 			}
// 			
//
// 			int count = 0;
// 			fieldModified.ForEach((item)=>
// 			{
// 				count += item.Count(p => p == 1);
// 			});
//
// 			lst.Add(count);
// 			if (count < min)
// 				min = count;
// 		}
// 		
// 		return results.Any() ? min.ToString() : "-1";
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