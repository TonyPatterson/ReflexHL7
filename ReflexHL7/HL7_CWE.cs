using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[HL7FieldDefinition]
[ExcludeFromCodeCoverage]
public partial class HL7_CWE
{
    [HL7Component(1)]
    public string? Identifier { get; private init; }

    [HL7Component(2)]
    public string? Text { get; private init; }

    [HL7Component(3)]
    public string? NameOfCodingSystem { get; private init; }

    [HL7Component(4)]
    public string? AlternateIdentifier { get; private init; }

    [HL7Component(5)]
    public string? AlternateText { get; private init; }

    [HL7Component(6)]
    public string? NameOfAlternateCodingSystem { get; private init; }

    [HL7Component(7)]
    public string? CodingSystemVersionId { get; private init; }

    [HL7Component(8)]
    public string? AlternateCodingSystemVersionId { get; private init; }

    [HL7Component(9)]
    public string? OriginalText { get; private init; }

    [HL7Component(10)]
    public string? SecondAlternateIdentifier { get; private init; }

    [HL7Component(11)]
    public string? SecondAlternateText { get; private init; }

    [HL7Component(12)]
    public string? NameOfSecondAlternateCodingSystem { get; private init; }

    [HL7Component(13)]
    public string? SecondAlternateCodingSystemVersionId { get; private init; }

    [HL7Component(14)]
    public string? CodingSystemOid { get; private init; }

    [HL7Component(15)]
    public string? ValueSetOid { get; private init; }

    [HL7Component(16)]
    public HL7_DTM? ValueSetVersionId { get; private init; }

    [HL7Component(17)]
    public string? AlternateCodingSystemOid { get; private init; }

    [HL7Component(18)]
    public string? AlternateValueSetOid { get; private init; }

    [HL7Component(19)]
    public HL7_DTM? AlternateValueSetVersionId { get; private init; }

    [HL7Component(20)]
    public string? SecondAlternateCodingSystemOid { get; private init; }

    [HL7Component(21)]
    public string? SecondAlternateValueSetOid { get; private init; }

    [HL7Component(22)]
    public HL7_DTM? SecondAlternateValueSetVersionId { get; private init; }
}