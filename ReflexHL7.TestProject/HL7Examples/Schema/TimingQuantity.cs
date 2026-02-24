using ReflexHL7;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition(TrailingSegments = "TQ1,TQ2")]
public partial class TimingQuantity
{
}