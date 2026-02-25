namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7FieldDefinition]
public partial class HL7_XPN
{
    [HL7Component(1)]
    public string? FamilyName { get; init; }

    [HL7Component(2)]
    public string? GivenName { get; init; }

    [HL7Component(3)]
    public string? FurtherGivenNamesOrInitials { get; init; }

    [HL7Component(4)]
    public string? Suffix { get; init; }

    [HL7Component(5)]
    public string? Prefix { get; init; }

    [HL7Component(6)]
    public string? Degree { get; init; }

    [HL7Component(7)]
    public string? NameTypeCode { get; init; }

    [HL7Component(8)]
    public string? NameRepresentationCode { get; init; }

    [HL7Component(9)]
    public string? NameContext { get; init; }

    [HL7Component(10)]
    public string? NameValidityRange { get; init; }

    [HL7Component(11)]
    public string? NameAssemblyOrder { get; init; }

    [HL7Component(12)]
    public string? EffectiveDate { get; init; }

    [HL7Component(13)]
    public string? ExpirationDate { get; init; }

    [HL7Component(14)]
    public string? ProfessionalSuffix { get; init; }

    [HL7Component(15)]
    public string? CalledBy { get; init; }
}