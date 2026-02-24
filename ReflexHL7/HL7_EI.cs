using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[HL7FieldDefinition]
[ExcludeFromCodeCoverage]
public partial class HL7_EI
{
    [HL7Component(1)]
    public required string EntityIdentifier { get; init; }

    [HL7Component(2)]
    public required string NamespaceId { get; init; }

    [HL7Component(3)]
    public required string UniversalId { get; init; }

    [HL7Component(4)]
    public required string UniversalIdType { get; init; }
}