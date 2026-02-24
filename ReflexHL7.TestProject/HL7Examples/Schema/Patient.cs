using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition(TrailingSegments = "IN1,IN2,IN3")]
[ExcludeFromCodeCoverage]
public partial class Patient
{
    public required HL7_PID Pid { get; init; }

    [HL7SkipSegments("PD1,PRT,OH1,OH2,OH3,OH4")]
    public required IReadOnlyList<HL7_NTE> Nte { get; init; }

    [HL7SkipSegments("NK1,OH2,OH3,ARV")]
    public required PatientObservation PatientObservation { get; init; }

    public required Visit Visit { get; init; }
}