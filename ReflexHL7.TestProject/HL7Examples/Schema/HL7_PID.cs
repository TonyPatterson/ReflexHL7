using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7SegmentDefinition("PID")]
[ExcludeFromCodeCoverage]
public partial class HL7_PID
{
    [HL7Field(1)]
    public int SetId { get; init; }

    [HL7Field(2)]
    public string? PatientId { get; set; }

    [HL7Field(3, IsCollection = true)]
    public HL7_CX[]? PatientIdentifierList { get; init; }

    [HL7Field(4)]
    public string? AlternatePatientId { get; init; }

    [HL7Field(5)]
    public HL7_XPN? PatientName { get; init; }

    [HL7Field(6)]
    public HL7_XPN? MothersMaidenName { get; init; }

    [HL7Field(7)]
    public HL7_DTM? DateTimeOfBirth { get; init; }

    [HL7Field(8)]
    public string? AdministrativeSex { get; init; }

    [HL7Field(9)]
    public HL7_XPN? PatientAlias { get; init; }

    [HL7Field(10)]
    public required string?[] Race { get; init; }

    [HL7Field(11)]
    public required string?[] PatientAddress { get; init; }

    [HL7Field(12)]
    public string? CountyCode { get; init; }

    [HL7Field(13)]
    public required string?[] HomePhoneNumber { get; init; }

    [HL7Field(14)]
    public required string?[] BusinessPhoneNumber { get; init; }

    [HL7Field(15)]
    public string? PrimaryLanguage { get; init; }

    [HL7Field(16)]
    public string?[]? MaritalStatus { get; init; }

    [HL7Field(17)]
    public string? Religion { get; init; }

    [HL7Field(18)]
    public string? PatientAccountNumber { get; init; }

    [HL7Field(19)]
    public string? PatientSsnNumber { get; init; }

    [HL7Field(20)]
    public string? PatientDrivingLicence { get; init; }

    [HL7Field(21)]
    public string? MothersPatientDrivingLicence { get; init; }

    [HL7Field(22)]
    public required string?[] EthnicGroup { get; init; }

    [HL7Field(23)]
    public string? BirthPlace { get; init; }

    [HL7Field(24)]
    public string? MultipleBirthIndicator { get; init; }

    [HL7Field(25)]
    public string?[]? BirthOrder { get; init; }

    [HL7Field(26)]
    public string? Citizenship { get; init; }

    [HL7Field(27)]
    public string? VeteransMilitaryStatus { get; init; }

    [HL7Field(28)]
    public string? Nationality { get; init; }

    [HL7Field(29)]
    public string? PatientDeathDateTime { get; init; }

    [HL7Field(30)]
    public string? PatientDeathIndicator { get; init; }

    [HL7Field(31)]
    public string? IdentityUnknownIndicator { get; init; }

    [HL7Field(32)]
    public string? IdentityReliabilityCode { get; init; }

    [HL7Field(33)]
    public string? LastUpdateDateTime { get; init; }

    [HL7Field(34)]
    public string? LastUpdateFacility { get; init; }

    [HL7Field(35)]
    public string? TaxonomicClassificationCode { get; init; }

    [HL7Field(36)]
    public string? BreedCode { get; init; }

    [HL7Field(37)]
    public string? Strain { get; init; }

    [HL7Field(38)]
    public string? ProductionClassCode { get; init; }

    [HL7Field(39)]
    public string? TribalCitizenship { get; init; }

    [HL7Field(40)]
    public string? PatientTelecommunicationInformation { get; init; }
}