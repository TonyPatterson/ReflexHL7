using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[HL7ComponentDefinition]
[ExcludeFromCodeCoverage]
public partial class HL7_CWE_Component
{
    [HL7SubComponent(1)]
    public string? Identifier { get; private init; }

    [HL7SubComponent(2)]
    public string? Text { get; private init; }

    [HL7SubComponent(3)]
    public string? NameOfCodingSystem { get; private init; }

    [HL7SubComponent(4)]
    public string? AlternateIdentifier { get; private init; }

    [HL7SubComponent(5)]
    public string? AlternateText { get; private init; }

    [HL7SubComponent(6)]
    public string? NameOfAlternateCodingSystem { get; private init; }

    [HL7SubComponent(7)]
    public string? CodingSystemVersionId { get; private init; }

    [HL7SubComponent(8)]
    public string? AlternateCodingSystemVersionId { get; private init; }

    [HL7SubComponent(9)]
    public string? OriginalText { get; private init; }

    [HL7SubComponent(10)]
    public string? SecondAlternateIdentifier { get; private init; }

    [HL7SubComponent(11)]
    public string? SecondAlternateText { get; private init; }

    [HL7SubComponent(12)]
    public string? NameOfSecondAlternateCodingSystem { get; private init; }

    [HL7SubComponent(13)]
    public string? SecondAlternateCodingSystemVersionId { get; private init; }

    [HL7SubComponent(14)]
    public string? CodingSystemOid { get; private init; }

    [HL7SubComponent(15)]
    public string? ValueSetOid { get; private init; }

    [HL7SubComponent(16)]
    public HL7_DTM? ValueSetVersionId { get; private init; }

    [HL7SubComponent(17)]
    public string? AlternateCodingSystemOid { get; private init; }

    [HL7SubComponent(18)]
    public string? AlternateValueSetOid { get; private init; }

    [HL7SubComponent(19)]
    public HL7_DTM? AlternateValueSetVersionId { get; private init; }

    [HL7SubComponent(20)]
    public string? SecondAlternateCodingSystemOid { get; private init; }

    [HL7SubComponent(21)]
    public string? SecondAlternateValueSetOid { get; private init; }

    [HL7SubComponent(22)]
    public HL7_DTM? SecondAlternateValueSetVersionId { get; private init; }
}