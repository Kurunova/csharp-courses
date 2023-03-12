using System.Diagnostics;
using NUnit.Framework;

namespace Competition.Tests
{
	[TestFixture]
	public class JTest : BaseTest
	{
		protected override string FolderName => "problem-j-tests";
		protected static int TestCount => 25;
		
		//[TestCaseSource(nameof(GetPrepTestData))]
		[TestCase("05", "05.a")]
		//[TestCase("16", "16.a")]
		public void CheckFromFile(string fileInput, string fileOutput)
		{
			var path = Path.Combine(DirectoryName, fileInput);
			var lines = File.ReadLines(path).ToList();

			var sw = new Stopwatch();
			sw.Start();
			
			var results = new List<string>();

			var linesPosition = 0;
			
			var dict = new List<string>();
			int inputCount = Convert.ToInt32(lines[linesPosition].Trim());
			linesPosition++;
			
			for (int i = 0; i < inputCount; i++)
			{
				var data = lines[linesPosition].TrimEnd();
				linesPosition++;
				dict.Add(data);
			}
		
			int count = Convert.ToInt32(lines[linesPosition].Trim());
			linesPosition++;
			
			for (int j = 0; j < count; j++)
			{
				var data = lines[linesPosition].TrimEnd();
				linesPosition++;
				
				var result = JSolution.Execute(data, dict);
				results.Add(result);
			}

			Solution.PrintResults(results);

			sw.Stop();
			Console.WriteLine("Elapsed={0}",sw.Elapsed);

			Assert.LessOrEqual(sw.Elapsed, new TimeSpan(0 ,0, 2));
			
			CheckResults(fileOutput, results);
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