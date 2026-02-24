using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[HL7FieldDefinition]
[ExcludeFromCodeCoverage]
public partial class HL7_XON
{
    [HL7Component(1)]
    public string? OrganizationName { get; private init; }

    [HL7Component(2)]
    public string? OrganizationNameTypeCode { get; private init; }

    [HL7Component(6)]
    public HL7_HD_Component? AssigningAuthority { get; private init; }

    [HL7Component(7)]
    public string? IdentifierTypeCode { get; private init; }

    [HL7Component(8)]
    public HL7_HD_Component? AssigningFacility { get; private init; }

    [HL7Component(9)]
    public string? NameRepresentationCode { get; private init; }

    [HL7Component(10)]
    public string? OrganizationIdentifier { get; private init; }
}