``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2486/22H2/2022Update)
AMD Ryzen 7 4800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
  DefaultJob : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2


```
|     Method | val |      Mean |     Error |    StdDev |
|----------- |---- |----------:|----------:|----------:|
| CalcStruct |  10 |  8.937 ns | 0.1449 ns | 0.1284 ns |
|  CalcClass |  10 | 50.680 ns | 0.9825 ns | 0.9190 ns |
