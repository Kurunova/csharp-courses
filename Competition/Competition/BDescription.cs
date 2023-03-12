﻿// B. Морской бой (10 баллов)
// ограничение по времени на тест2 секунды
// ограничение по памяти на тест512 мегабайт
// вводстандартный ввод
// выводстандартный вывод
// Это наиболее простая задача Контеста. Надеемся, что вы познакомились с тестирующей системой Codeforces, приняв участие в Песочнице. В любом случае напоминаем вам несколько моментов.
//
// Правильные решения задач должны проходить все заранее заготовленные тесты жюри и укладываться в ограничения по времени/памяти на каждом тесте. В некоторых задачах допустимы частичные решения (это написано в условии).
//
// Ниже перечислены технические требования к решениям:
//
// решение располагается в одном файле исходного кода;
// решение читает входные данные со стандартного ввода (экрана);
// решение пишет выходные данные на стандартный вывод (экран);
// решение не взаимодействует как-либо с другими ресурсами компьютера (сеть, жесткий диск, процессы и прочее);
// решение использует только стандартную библиотеку языка;
// решение располагается в пакете по-умолчанию (или его аналоге для вашего языка), имеют стандартную точку входа для консольных программ;
// гарантируется, что во всех тестах выполняются все ограничения, что содержатся в условии задачи — как-либо проверять входные данные на корректность не надо, все тесты строго соответствуют описанному в задаче формату;
// выводи ответ в точности в том формате, как написано в условии задачи (не надо выводить «поясняющих» комментариев типа введите число или ответ равен);
// решения можно отправлять сколько угодно раз (пожалуйста, только без абьюза системы).
// Для вашего удобства большинство тестов, на которых будут тестироваться ваши решения, являются открытыми. В каждой задаче можно скачать архив тестов (смотрите сайдбар справа, раздел «Материалы соревнования»).
//
// Обратите внимание на SQL-задачу. Не забудьте попробовать свои силы в её решении.
//
// Перейдём к задаче.
//
// Вы участвуете в разработке подсистемы проверки поля для игры «Морской бой». Вам требуется написать проверку корректности количества кораблей на поле, учитывая их размеры. Напомним, что на поле должны быть:
//
// четыре однопалубных корабля,
// три двухпалубных корабля,
// два трёхпалубных корабля,
// один четырёхпалубный корабль.
// Вам заданы 10
//  целых чисел от 1
//  до 4
// . Проверьте, что заданные размеры соответствуют требованиям выше.
//
// Входные данные
// В первой строке записано целое число t
//  (1≤t≤1000
// ) — количество наборов входных данных в тесте.
//
// Наборы входных данных в тесте независимы. Друг на друга они никак не влияют.
//
// Каждый набор входных данных состоит из одной строки, которая содержит 10
//  целых чисел a1,a2,…,a10
//  (1≤ai≤4
// ) — размеры кораблей на поле в произвольном порядке.
//
// Обратите внимание, что уже гарантируется, что кораблей на поле ровно 10
//  и их размеры от 1
//  до 4
// , включительно. Вам необходимо проверить, что количество кораблей каждого типа соответствует правилам игры.
//
// Выходные данные
// Для каждого набора входных данных в отдельной строке выведите:
//
// YES, если заданные размеры кораблей на поле соответствуют правилам игры;
// NO в противном случае.
// Вы можете выводить YES и NO в любом регистре (например, строки yEs, yes, Yes и YES будут распознаны как положительный ответ).
//
// Пример
// входные данныеСкопировать
// 5
// 2 1 3 1 2 3 1 1 4 2
// 1 1 1 2 2 2 3 3 3 4
// 1 1 1 1 2 2 2 3 3 4
// 4 3 3 2 2 2 1 1 1 1
// 4 4 4 4 4 4 4 4 4 4
// выходные данныеСкопировать
// YES
// NO
// YES
// YES
// NO