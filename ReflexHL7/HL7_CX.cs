using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7FieldDefinition]
[ExcludeFromCodeCoverage]
public partial class HL7_CX
{
    [HL7Component(1)]
    public string? IdNumber { get; set; }

    [HL7Component(2)]
    public string? IdentifierCheckDigit { get; set; }

    [HL7Component(3)]
    public string? CheckDigitScheme { get; set; }

    [HL7Component(4)]
    public string? AssigningAuthority { get; set; }

    [HL7Component(5)]
    public string? IdentifierType { get; set; }

    [HL7Component(6)]
    public string? AssigningFacility { get; set; }

    [HL7Component(7)]
    public HL7_DTM? EffectiveDate { get; set; }

    [HL7Component(8)]
    public HL7_DTM? ExpirationDate { get; set; }

    [HL7Component(9)]
    public string? AssigningJurisdiction { get; set; }

    [HL7Component(10)]
    public string? AssigningAgencyOrDepartment { get; set; }

    [HL7Component(11)]
    public string? SecurityCheck { get; set; }

    [HL7Component(12)]
    public string? SecurityCheckScheme { get; set; }
}