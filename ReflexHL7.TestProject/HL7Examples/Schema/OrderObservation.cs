using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition]
[ExcludeFromCodeCoverage]
public partial class OrderObservation
{
    public required CommonOrder CommonOrder { get; init; }

    public required HL7_OBR Obr { get; init; }

    public required IReadOnlyList<HL7_NTE> Nte { get; init; }

    public required ObservationParticipation ObservationParticipation { get; init; }

    public required TimingQuantity TimingQuantity { get; init; }

    [HL7SkipSegments("CTD")]
    public required IReadOnlyList<Observation> Observation { get; init; }

    [HL7SkipSegments("FT1,CTI")]
    public required Specimen Specimen { get; init; }
}