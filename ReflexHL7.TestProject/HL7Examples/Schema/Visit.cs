using ReflexHL7;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition(TrailingSegments = "PV1,PV2,PRT")]
public partial class Visit
{
}