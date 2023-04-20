// H. Валидация карты (25 баллов)
// ограничение по времени на тест1 секунда
// ограничение по памяти на тест512 мегабайт
// вводстандартный ввод
// выводстандартный вывод
// В этой задаче вам необходимо реализовать валидацию корректности карты для стратегической компьютерной игры.
//
// Карта состоит из гексагонов (шестиугольников), каждый из которых принадлежит какому-то региону карты. В файлах игры карта представлена как n
//  строк по m
//  символов в каждой (строки и символы в них нумеруются с единицы). Каждый нечетный символ каждой четной строки и каждый четный символ каждой нечетной строки — точка (символ . с ASCII кодом 46
// ); все остальные символы соответствуют гексагонам и являются заглавными буквами латинского алфавита. Буква указывает на то, какому региону принадлежит гексагон.
//
// Посмотрите на картинку ниже, чтобы понять, как описание карты в файлах игры соответствует карте из шестиугольников.
//
// Соответствие описания карты в файле (слева) и самой карты (справа). Регионы R, G, V, Y и B окрашены в красный, зеленый, фиолетовый, желтый и синий цвет, соответственно.
// Вы должны проверить, что каждый регион карты является одной связной областью. Иными словами, не должно быть двух гексагонов, принадлежащих одному и тому же региону, которые не соединены другими гексагонами этого же региона.
//
// Карта слева является корректной. Карта справа не является корректной, так как гексагоны, обозначенные цифрами 1
//  и 2
// , принадлежат одному и тому же региону (обозначенному красным цветом), но не соединены другими гексагонами этого региона.
// Неполные решения этой задачи (например, недостаточно эффективные) могут быть оценены частичным баллом.
//
// Входные данные
// В первой строке задано одно целое число t
//  (1≤t≤100
// ) — количество наборов входных данных.
//
// Первая строка набора входных данных содержит два целых числа n
//  и m
//  (2≤n,m≤20
// ) — количество строк и количество символов в каждой строке в описании карты.
//
// Далее следуют n
//  строк по m
//  символов в каждой — описание карты. Каждый нечетный символ каждой четной строки и каждый четный символ каждой нечетной строки — точка (символ . с ASCII кодом 46
// ); все остальные символы соответствуют гексагонам и являются заглавными буквами латинского алфавита.
//
// Выходные данные
// На каждый набор входных данных выведите ответ в отдельной строке — YES, если каждый регион карты представляет связную область, или NO, если это не так.
//
// Пример
// входные данныеСкопировать
// 3
// 3 7
// R.R.R.G
// .Y.G.G.
// B.Y.V.V
// 4 8
// Y.R.B.B.
// .R.R.B.V
// B.R.B.R.
// .B.B.R.R
// 2 7
// G.B.R.G
// .G.G.G.
// выходные данныеСкопировать
// YES
// NO
// YES
// Примечание
// Первые два набора входных данных из примера показаны на второй картинке в условии.

//
// using System.Collections;
//
// class Result
// {
// 	public static string Execute(char[,] data, int rowCount, int columnCount)
// 	{
// 		bool result = true;
//
// 		List<char> processed = new List<char>();
// 		bool?[,] validation = new bool?[rowCount,columnCount];
// 		
// 		for (int i = 0; i < rowCount; i++)
// 		{
// 			for (int j = i%2 == 0 ? 0 : 1; j < columnCount; j+=2)
// 			{
// 				var validated = validation[i, j];
// 				if(validated == true)
// 					continue;
// 				
// 				var color = data[i, j];
// 				if (processed.Any(p => p == color))
// 				{
// 					result = false;
// 					break;
// 				}
//
// 				CheckValidation(ref data, ref validation, color, i, j, rowCount, columnCount);
//
// 				processed.Add(color);
// 				
// 			}
// 			
// 			if(result == false)
// 				break;
// 		}
// 		
// 		return result ? "YES" : "NO";
// 	}
//
// 	private static void CheckValidation(ref char[,] data, ref bool?[,] validation, char color, int i, int j, int rowCount, int columnCount)
// 	{
// 		if (validation[i, j] == true || data[i, j] != color)
// 			return;
// 		
// 		validation[i, j] = true;
// 		
// 		
// 		if (j + 2 < columnCount)
// 		{
// 			CheckValidation(ref data, ref validation, color, i, j + 2, rowCount, columnCount);
// 		}
// 		
// 		if (i + 1 < rowCount && j + 1 < columnCount)
// 		{
// 			CheckValidation(ref data, ref validation, color, i + 1, j + 1, rowCount, columnCount);
// 		}
// 		
// 		if (i + 1 < rowCount && j - 1 >= 0)
// 		{
// 			CheckValidation(ref data, ref validation, color, i + 1, j - 1, rowCount, columnCount);
// 		}
// 		
// 		if (j - 2 >= 0)
// 		{
// 			CheckValidation(ref data, ref validation, color, i, j - 2, rowCount, columnCount);
// 		}
// 		
// 		if (i - 1 >= 0 && j + 1 < columnCount)
// 		{
// 			CheckValidation(ref data, ref validation, color, i - 1, j + 1, rowCount, columnCount);
// 		}
// 		
// 		if (i - 1 >= 0 && j - 1 >= 0)
// 		{
// 			CheckValidation(ref data, ref validation, color, i - 1, j - 1, rowCount, columnCount);
// 		}
// 	}
// }
//
// class Solution
// {
// 	public static void Main(string[] args)
// 	{
// 		var results = new List<string>();
// 		
// 		int inputCount = Convert.ToInt32(Console.ReadLine().Trim());
// 		for (int i = 0; i < inputCount; i++)
// 		{
// 			var count = Console.ReadLine().Trim().Split(" ").Select(p => Convert.ToInt32(p)).ToArray();
// 			
// 			char[,] array = new char[count[0], count[1]];
//
// 			for (int j = 0; j < count[0]; j++)
// 			{
// 				var data = Console.ReadLine().TrimEnd().ToCharArray();
// 				for (int k = 0; k < count[1]; k++)
// 				{
// 					array[j, k] = data[k];
// 				}
// 			}
// 			
// 			string result = Result.Execute(array, count[0], count[1]);
// 			results.Add(result);
// 		}
// 		
// 		foreach (var result in results)
// 		{
// 			Console.WriteLine(result);
// 		}
// 	}
// }