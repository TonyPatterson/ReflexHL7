using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7MessageDefinition]
[ExcludeFromCodeCoverage]
public partial class PatientResult
{
    public required Patient Patient { get; init; }

    public required IReadOnlyList<OrderObservation> OrderObservation { get; init; }

    public required Device Device { get; init; }
}