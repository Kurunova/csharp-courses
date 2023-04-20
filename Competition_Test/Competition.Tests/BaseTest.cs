using System.Reflection;
using NUnit.Framework;

namespace Competition.Tests
{
	public abstract class BaseTest
	{
		private string _directoryName;
		protected string DirectoryName => _directoryName ??= Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Source", FolderName);
		
		protected virtual string FolderName => GetType().Name.Replace("Test","");
		
		protected void CheckResults(string fileOutput, List<string> results)
		{
			var path = Path.Combine(DirectoryName, fileOutput);
			var lines = File.ReadLines(path).ToList();

			for (int i = 0; i < results.Count; i++)
			{
				Assert.AreEqual(lines[i], results[i]);
			}
		}
	}
}