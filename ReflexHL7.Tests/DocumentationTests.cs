#pragma warning disable SA1402 // File may only contain a single type

namespace ReflexHL7.Tests;

public class DocumentationTests
{
    [Fact]
    public void SimpleExampleTest()
    {
        using var s = File.OpenText("Data\\ORU_R01.hl7");

        var msg = HL7_VanillaMessage.Read(s);

        Console.WriteLine($"Patient account number: {msg.PID.PatientAccountNumber}");

        Assert.Equal("12345566", msg.PID.PatientAccountNumber);
    }
}

[HL7MessageDefinition]
public partial class HL7_VanillaMessage
{
    [HL7Segment("PID")]
    public required HL7_PID_Minimal PID { get; init; }
}

[HL7SegmentDefinition("PID")]
public partial class HL7_PID_Minimal
{
    [HL7Field(18)]
    public string? PatientAccountNumber { get; init; }
}