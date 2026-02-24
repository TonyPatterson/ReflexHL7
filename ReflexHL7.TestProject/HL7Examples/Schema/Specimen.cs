using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition]
[ExcludeFromCodeCoverage]
public partial class Specimen
{
    [HL7SkipSegments("SPM")]
    public required SpecimenObservation SpecimenObservation { get; init; }
}