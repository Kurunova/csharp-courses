``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2486/22H2/2022Update)
AMD Ryzen 7 4800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
  DefaultJob : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2


```
|            Method |     Mean |     Error |    StdDev |   Gen0 | Allocated |
|------------------ |---------:|----------:|----------:|-------:|----------:|
| TestInternalClass | 1.846 μs | 0.0353 μs | 0.0434 μs | 7.9346 |   16.2 KB |
|  TestDefaultClass | 1.049 μs | 0.0063 μs | 0.0059 μs | 5.6381 |  11.52 KB |
