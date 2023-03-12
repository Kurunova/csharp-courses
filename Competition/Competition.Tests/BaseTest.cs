using System.Reflection;
using NUnit.Framework;

namespace Competition.Tests
{
	public abstract class BaseTest
	{
		private string _directoryName;
		protected string DirectoryName => _directoryName ??= Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Source", FolderName);
		
		protected virtual string FolderName => GetType().Name.Replace("Test","");
		
		private TestContext testContextInstance;
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}
		
		protected void CheckResults(string fileOutput, List<string> results)
		{
			var path = Path.Combine(DirectoryName, fileOutput);
			var lines = File.ReadLines(path).ToList();

			for (int i = 0; i < results.Count; i++)
			{
				TestContext.WriteLine($"expected {lines[i]}, result = {results[i]}");
				Assert.AreEqual(lines[i], results[i]);
			}
		}
	}
}