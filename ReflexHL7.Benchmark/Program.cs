using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using ReflexHL7.Benchmark;
using System.Text;
using System.Text.Json;

#if DEBUG
var runner = new ReflexHL7Benchmarks();

runner.Setup();

var result = runner.ParseOruR01();

string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });

File.WriteAllText(@"..\..\..\Converted.json", json);

#else

BenchmarkRunner.Run(typeof(Program).Assembly);

#endif