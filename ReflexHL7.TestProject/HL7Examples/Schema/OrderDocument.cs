using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition(TrailingSegments = "PRT,TXA")]
[ExcludeFromCodeCoverage]
public partial class OrderDocument
{
    public required HL7_OBX Obx { get; init; }
}