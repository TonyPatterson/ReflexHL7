using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition(AllowVersions = "2.3.1|2.4|2.5.1")]
[ExcludeFromCodeCoverage]
public partial class HL7_ORU_R01_Partial
{
    [HL7Segment("MSH")]
    [HL7SkipSegments("ARV,SFT,UAC")]
    public required HL7_MSH MSH { get; init; }

    public required IReadOnlyList<PatientResult> PatientResult { get; init; }
}