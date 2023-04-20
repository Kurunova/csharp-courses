// J. Рифмы (30 баллов)
// ограничение по времени на тест2 секунды
// ограничение по памяти на тест512 мегабайт
// вводстандартный ввод
// выводстандартный вывод
// Вы разрабатываете программу автоматической генерации стихотворений. Один из модулей этой программы должен подбирать рифмы к словам из некоторого словаря.
//
// Словарь содержит n
//  различных слов. Словами будем называть последовательности из 1
// —10
//  строчных букв латинского алфавита.
//
// Зарифмованность двух слов — это длина их наибольшего общего суффикса (суффиксом будем называть какое-то количество букв в конце слова). Например:
//
// task и flask имеют зарифмованность 3
//  (наибольший общий суффикс — ask);
// decide и code имеют зарифмованность 2
//  (наибольший общий суффикс — de);
// id и void имеют зарифмованность 2
//  (наибольший общий суффикс — id);
// code и forces имеют зарифмованность 0
// .
// Ваша программа должна обработать q
//  запросов следующего вида: дано слово ti
//  (возможно, принадлежащее словарю), необходимо найти слово из словаря, которое не совпадает с ti
//  и имеет максимальную зарифмованность с ti
//  среди всех слов словаря, не совпадающих с ti
// . Если подходящих слов несколько — выведите любое из них.
//
// Неполные решения этой задачи (например, недостаточно эффективные) могут быть оценены частичным баллом.
//
// Входные данные
// Первая строка содержит одно целое число n
//  (2≤n≤50000
// ) — размер словаря.
//
// Далее следуют n
//  строк, i
// -я строка содержит одну строку si
//  (1≤|si|≤10
// ) — i
// -е слово из словаря. В словаре все слова различны.
//
// Следующая строка содержит одно целое число q
//  (1≤q≤50000
// ) — количество запросов.
//
// Далее следуют q
//  строк, i
// -я строка содержит одну строку ti
//  (1≤|ti|≤10
// ) — i
// -й запрос.
//
// Каждая строка si
//  и каждая строка ti
//  состоит только из строчных букв латинского алфавита.
//
// Выходные данные
// Для каждого запроса выведите одну строку — слово из словаря, которое не совпадает с заданным в запросе и имеет с ним максимальную зарифмованность (если таких несколько — выведите любое).
//
// Пример
// входные данныеСкопировать
// 3
// task
// decide
// id
// 6
// flask
// code
// void
// forces
// id
// ask
// выходные данныеСкопировать
// task
// decide
// id
// task
// decide
// task


// class Result
// {
// 	public static string Execute(string word, List<string> dict, List<string> dictReverse)
// 	{
// 		int maxWordIndex = 0;
// 		int maxMatchSymbols = 0;
//
// 		string wordReverse = Reverse(word);
// 		
// 		for (int i = 0; i < dictReverse.Count; i++)
// 		{
// 			var dictWord = dictReverse[i];
//
// 			if (dictWord == wordReverse)
// 				continue;
//
// 			var matchSymbols = MaxMatchSymbols2(dictWord, wordReverse);
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
// 		string result = dict[maxWordIndex];
// 		
// 		return result;
// 	}
//
// 	public static int MaxMatchSymbols2(string dictWord, string word)
// 	{
// 		int result = 0;
// 		
// 		for (int i = 0; i < Math.Min(word.Length, dictWord.Length); i++)
// 		{
// 			if (word[i] != dictWord[i])
// 				break;
// 			
// 			result++;
// 		}
//
// 		return result;
// 	}
//
// 	public static int MaxMatchSymbols(string dictWord, string word, int? countMax = null)
// 	{
// 		int result = 0;
// 		
// 		for (int i = 0; i < (countMax ?? word.Length); i++)
// 		{
// 			for (int j = 0; j < (countMax ?? dictWord.Length); j++)
// 			{
// 				if (word[i] == dictWord[j])
// 				{
// 					string test = $"{word[i]}";
// 					int currLength = 1;
// 					for (int k = 0; k < Math.Min(word.Length - i, dictWord.Length - j) - 1; k++)
// 					{
// 						if (word[i + currLength] != dictWord[j + currLength])
// 							break;
//
// 						test += word[i + currLength];
// 						currLength++;
// 					}
//
// 					
// 					//Console.WriteLine($"test {test}");
// 					
// 					if (currLength > result)
// 						result = currLength;
// 				}
// 			}
// 			
// 		}
//
// 		return result;
// 	}
// 	
// 	public static string Reverse(string s)
// 	{
// 		char[] charArray = s.ToCharArray();
// 		Array.Reverse(charArray);
// 		return new string(charArray);
// 	}
// }
//
// class Solution
// {
// 	public static void Main(string[] args)
// 	{
// 		int inputCount = Convert.ToInt32(Console.ReadLine().Trim());
// 		var results = new List<string>();
// 			
// 		var dict = new List<string>();
// 		var dictReverse = new List<string>();
// 		for (int i = 0; i < inputCount; i++)
// 		{
// 			var data = Console.ReadLine().TrimEnd();
// 			dict.Add(data);
// 			dictReverse.Add(Result.Reverse(data));
// 		}
// 		
// 		int count = Convert.ToInt32(Console.ReadLine().Trim());
//
// 		for (int j = 0; j < count; j++)
// 		{
// 			var data = Console.ReadLine().TrimEnd();
// 			string result = Result.Execute(data, dict, dictReverse);
// 			results.Add(result);
// 		}
// 		
// 		foreach (var result in results)
// 		{
// 			Console.WriteLine(result);
// 		}
// 	}
// }