// namespace Competition;
//
// public class JSolution : Solution
// {
// 	public static string Execute(string word, List<string> dict)
// 	{
// 		int maxWordIndex = 0;
// 		int maxMatchSymbols = 0;
//
// 		for (int i = 0; i < dict.Count; i++)
// 		{
// 			var dictWord = dict[i];
//
// 			if (dictWord == word)
// 				continue;
//
// 			var matchSymbols = MaxMatchSymbols(dictWord, word);
//
// 			//Console.WriteLine($"Word {word} has match symbols {matchSymbols} in {dictWord}");
// 			
// 			if (matchSymbols > 0 && matchSymbols >= maxMatchSymbols)
// 			{
// 				maxMatchSymbols = matchSymbols;
// 				maxWordIndex = i;
// 			}
// 		}
// 		
// 		return dict[maxWordIndex];
// 	}
//
// 	
// 	public static int MaxMatchSymbols(string dictWord, string word)
// 	{
// 		int result = 0;
//
// 		var wordIndex = word.Length;
// 		var dictWordIndex = dictWord.Length;
// 		
// 		for (int i = 0; i < Math.Min(word.Length, dictWord.Length); i++)
// 		{
// 			wordIndex--;
// 			dictWordIndex--;
// 			
// 			if (word[wordIndex] != dictWord[dictWordIndex])
// 				break;
// 			
// 			result++;
// 		}
//
// 		return result;
// 	}
// }