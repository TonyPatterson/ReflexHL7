using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition]
[ExcludeFromCodeCoverage]
public partial class Observation
{
    public HL7_OBX? Obx { get; private init; }

    [HL7SkipSegments("PRT")]
    public IReadOnlyList<HL7_NTE>? Nte { get; private init; }
}