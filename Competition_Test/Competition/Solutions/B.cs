﻿// B. Сумма к оплате (10 баллов)
// ограничение по времени на тест1 секунда
// ограничение по памяти на тест256 мегабайт
// вводстандартный ввод
// выводстандартный вывод
// В магазине акция: «купи три одинаковых товара и заплати только за два». Конечно, каждый купленный товар может участвовать лишь в одной акции. Акцию можно использовать многократно.
//
// 	Например, если будут куплены 7 товаров одного вида по цене 2 за штуку и 5 товаров другого вида по цене 3 за штуку, то вместо 7⋅2+5⋅3 надо будет оплатить 5⋅2+4⋅3=22.
//
// 	Считая, что одинаковые цены имеют только одинаковые товары, найдите сумму к оплате.
//
// 	Неполные решения этой задачи (например, недостаточно эффективные) могут быть оценены частичным баллом.
//
// 	Входные данные
// В первой строке записано целое число t (1≤t≤104) — количество наборов входных данных.
//
// 	Далее записаны наборы входных данных. Каждый начинается строкой, которая содержит n (1≤n≤2⋅105) — количество купленных товаров. Следующая строка содержит их цены p1,p2,…,pn (1≤pi≤104). Если цены двух товаров одинаковые, то надо считать, что это один и тот товар.
//
// 	Гарантируется, что сумма значений n по всем тестам не превосходит 2⋅105.
//
// 	Выходные данные
// 	Выведите t целых чисел — суммы к оплате для каждого из наборов входных данных.
//
// 	Пример
// 	входные данныеСкопировать
// 6
// 12
// 2 2 2 2 2 2 2 3 3 3 3 3
// 12
// 2 3 2 3 2 2 3 2 3 2 2 3
// 1
// 10000
// 9
// 1 2 3 1 2 3 1 2 3
// 6
// 10000 10000 10000 10000 10000 10000
// 6
// 300 100 200 300 200 300
// выходные данныеСкопировать
// 22
// 22
// 10000
// 12
// 40000
// 1100

// class Result
// {
// 	public static int Execute(List<int> data)
// 	{
// 		int result = 0;
// 		
// 		var counts = data.GroupBy(x => x)
// 			.Select(x => new { price = x.Key, count = x.Count()});
// 		
// 		foreach (var item in counts)
// 		{
// 			result += item.price * GetNewCount(item.count);
// 		}
// 		return result;
// 	}
// 	
// 	public static int GetNewCount(int count)
// 	{
// 		var full = count / 3;
// 		var result = count - full;
// 		return result;
// 	}
// }
//
// class Solution
// {
// 	public static void Main(string[] args)
// 	{
// 		int inputCount = Convert.ToInt32(Console.ReadLine().Trim());
// 		var results = new List<int>();
// 			
// 			
// 		for (int i = 0; i < inputCount; i++)
// 		{
// 			var count = Console.ReadLine();
// 			List<int> data = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arTemp => Convert.ToInt32(arTemp)).ToList();
// 			int result = Result.Execute(data);
// 			results.Add(result);
// 		}
//
// 		foreach (var result in results)
// 		{
// 			Console.WriteLine(result);
// 		}
// 	}
// }