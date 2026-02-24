using BenchmarkDotNet.Attributes;
using ReflexHL7;
using ReflexHL7.TestProject.HL7Examples.Schema;

namespace ReflexHL7.Benchmark;

[MemoryDiagnoser]
public class ReflexHL7Benchmarks
{
    private string? _hl7Resource;

    [GlobalSetup]
    public void Setup()
    {
        _hl7Resource = File.ReadAllText(@"Data\ORU_R01.hl7");
    }

    [Benchmark]
    public HL7_ORU_R01_Partial ParseOruR01()
    {
        using var reader = new StringReader(_hl7Resource!);

        HL7Tokeniser tokeniser = new(reader);

        return HL7_ORU_R01_Partial.Read(tokeniser);
    }
}