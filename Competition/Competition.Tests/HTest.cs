using System.Diagnostics;
using NUnit.Framework;

namespace Competition.Tests
{
	[TestFixture]
	public class HTest : BaseTest
	{
		protected override string FolderName => "h-test";
		protected static int TestCount => 340;
		
		[TestCaseSource(nameof(GetPrepTestData))]
		//[TestCase("05", "05.a")]
		//[TestCase("16", "16.a")]
		public void CheckFromFile(string fileInput, string fileOutput)
		{
			var path = Path.Combine(DirectoryName, fileInput);
			var lines = File.ReadLines(path).ToList();

			var sw = new Stopwatch();
			sw.Start();

			var linesPosition = 0;
			
			var results = new List<string>();
		
			var field = new int[8, 8];
			for (int g = 0; g < 8; g++)
			{
				var line = lines[linesPosition].TrimEnd().ToCharArray().Select(p => p == '*' ? 1 : 0).ToArray();
				linesPosition++;
				for (int i = 0; i < line.Length; i++)
				{
					field[g, i] = line[i];
				}
			}
		
			var figures = new List<List<List<int>>>();
		
			int generalCount = Convert.ToInt32(lines[linesPosition].Trim());
			linesPosition++;
			for (int g = 0; g < generalCount; g++)
			{
				var figure = new List<List<int>>();

				int linesCount = Convert.ToInt32(lines[linesPosition].Trim());
				linesPosition++;
				for (int i = 0; i < linesCount; i++)
				{
					var line = lines[linesPosition].TrimEnd().ToCharArray().Select(p => p == '*' ? 1 : 0).ToList();
					linesPosition++;
					figure.Add(line);
				}
				figures.Add(figure);
			}

			var fields = new List<int[,]> { field };
		
			for (int i = 0; i < figures.Count; i++)
			{
				var modifiedFields = new List<int[,]>();
			
				do
				{
					var fieldToProcess = fields[0];
					fields.Remove(fieldToProcess);
					var executed = Execute(fieldToProcess, figures[i]);
					modifiedFields.AddRange(executed);
				} while (fields.Count > 0);
			
				fields.AddRange(modifiedFields);
			
				var r = GetMinimum(fields);
			
			}
		

			var getMin = GetMinimum(fields);
		
			results.Add(getMin.Item1.ToString());
			//PrintResults(results);			


			sw.Stop();
			TestContext.WriteLine("Elapsed={0}",sw.Elapsed);

			Assert.LessOrEqual(sw.Elapsed, new TimeSpan(0 ,0, 2));
			
			CheckResults(fileOutput, results);
		}
			
		private static List<int[,]> Execute(int[,] field, List<List<int>> figure)
		{
			var fieldRowCount = field.GetLength(0);
			var fieldColumnCount = field.GetLength(1);
			
			var figureRowCount = figure.Count;
			var figureColumnCount = figure[0].Count;

			var results = new List<(int row, int column)>();
			
			for (int i = 0; i < fieldRowCount - figureRowCount + 1; i++)
			{
				for (int j = 0; j < fieldColumnCount - figureColumnCount + 1; j++)
				{
					bool canPlace = true;
					for (int k = 0; k < figureRowCount; k++)
					{
						for (int l = 0; l < figureColumnCount; l++)
						{
							if(figure[k][l] == 0)
								continue;
							
							if(field[i + k, j + l] == 0)
								continue;

							canPlace = false;
						}
					}

					if (canPlace)
					{
						results.Add((i, j));
					}
				}
			}

		
			var fields = new List<int[,]>();
			foreach (var result in results)
			{
				var fieldModified = new int[fieldRowCount, fieldColumnCount];

				// count rows and columns
				var rowsSum = new int[fieldRowCount];
				var columnsSum = new int[fieldColumnCount];

				for (int i = 0; i < fieldRowCount; i++)
				{
					for (int j = 0; j < fieldColumnCount; j++)
					{
						fieldModified[i, j] = field[i, j];
						
						// put figure on field
						if ((result.row <= i && i < result.row + figureRowCount)
						    && (result.column <= j && j < result.column + figureColumnCount))
						{
							if(fieldModified[i, j] == 0 && figure[i - result.row][j - result.column] == 1)
								fieldModified[i, j] = figure[i - result.row][j - result.column];
						}

						rowsSum[i] += fieldModified[i, j];
						columnsSum[j] += fieldModified[i, j];
					}
				}

				// burn it
				var rowFullValue = rowsSum.Select((p, index) => new { Index = index, Value = p })
					.Where(p => p.Value == fieldRowCount);
				var columnFullValue = columnsSum.Select((p, index) => new { Index = index, Value = p })
					.Where(p => p.Value == fieldColumnCount);

				
				foreach (var rowFull in rowFullValue)
				{
					for (int j = 0; j < fieldColumnCount; j++)
					{
						fieldModified[rowFull.Index, j] = 0;
					}
				}
				
				foreach (var columnFull in columnFullValue)
				{
					for (int i = 0; i < fieldRowCount; i++)
					{
						fieldModified[i, columnFull.Index] = 0;
					}
				}
				
				fields.Add(fieldModified);
			}
			
			return fields;
		}

		private static (int, List<int>) GetMinimum(List<int[,]> fields)
		{
			var min = Int32.MaxValue;
			List<int> all = new List<int>();
			
			foreach (var field in fields)
			{
				var fieldRowCount = field.GetLength(0);
				var fieldColumnCount = field.GetLength(1);
				
				int count = 0;
				for (int i = 0; i < fieldRowCount; i++)
				{
					for (int j = 0; j < fieldColumnCount; j++)
					{
						count += field[i, j];
					}
				}
				
				all.Add(count);

				if (count < min)
				{
					min = count;
				}
			}
			return (min == Int32.MaxValue ? -1 : min, all);
		}

		private static void PrintResults(List<string> results)
		{		
			foreach (var result in results)
			{
				Console.WriteLine(result);
			}
		}		
		
		protected static IEnumerable<TestCaseData> GetPrepTestData()
		{
			for (int i = 1; i < TestCount + 1; i++)
			{
				yield return new TestCaseData($"{i:00}", $"{i:00}.a").SetName($"test {i:00}");
			}
		}
	}
}