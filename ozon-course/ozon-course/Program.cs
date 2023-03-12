// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

Summary[] summaries = BenchmarkRunner.Run(typeof(Program).Assembly);

Console.WriteLine("Hello, World!");