using ReflexHL7;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition(TrailingSegments = "NK1,OH2,OH3")]
public partial class NextOfKin
{
}