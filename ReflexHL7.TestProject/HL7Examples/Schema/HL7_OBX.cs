using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7SegmentDefinition("OBX")]
[ExcludeFromCodeCoverage]
public partial class HL7_OBX
{
    [HL7Field(1)]
    public int SetId { get; init; }

    [HL7Field(2)]
    public string? ValueTypeId { get; init; }

    [HL7Field(3)]
    public required string?[] ObservationIdentifier { get; init; }

    [HL7Field(4)]
    public string? ObservationSubIdentifier { get; init; }

    [HL7Field(5)]
    public required string?[] ObservationValue { get; init; }

    [HL7Field(6)]
    public required string?[] Units { get; init; }

    [HL7Field(7)]
    public string? ReferencesRange { get; init; }

    [HL7Field(8)]
    public string? InterpretationCodes { get; init; }

    [HL7Field(9)]
    public string? Probability { get; init; }

    [HL7Field(10)]
    public string? NatureOfAbnormalTest { get; init; }

    [HL7Field(11)]
    public string? ObservationResultStatus { get; init; }

    [HL7Field(12)]
    public string? EffectiveDateOfReferenceRange { get; init; }

    [HL7Field(13)]
    public string? UserDefinedAccessChecks { get; init; }

    [HL7Field(14)]
    public HL7_DTM? DateTimeOfTheObservation { get; init; }

    [HL7Field(15)]
    public string? ProducerId { get; init; }

    [HL7Field(16)]
    public string? ResponsibleObserver { get; init; }

    [HL7Field(17)]
    public string? ObservationMethod { get; init; }

    [HL7Field(18)]
    public string? EquipmentInstanceIdentifier { get; init; }

    [HL7Field(19)]
    public HL7_DTM? DateTimeOfTheAnalysis { get; init; }

    [HL7Field(20)]
    public string? ObservationSite { get; init; }

    [HL7Field(21)]
    public string? ObservationInstanceIdentifier { get; init; }

    [HL7Field(22)]
    public string? MoodCode { get; init; }

    [HL7Field(23)]
    public string? PerformingOrganizationName { get; init; }

    [HL7Field(24)]
    public string? PerformingOrganizationAddress { get; init; }

    [HL7Field(25)]
    public string? PerformingOrganizationMedicalDirector { get; init; }

    [HL7Field(26)]
    public string? PatientResultsReleaseCategory { get; init; }

    [HL7Field(27)]
    public string? RootCause { get; init; }

    [HL7Field(28)]
    public string? LocalProcessControl { get; init; }

    [HL7Field(29)]
    public string? ObservationType { get; init; }

    [HL7Field(30)]
    public string? ObservationSubType { get; init; }

    [HL7Field(31)]
    public string? ActionCodeId { get; init; }

    [HL7Field(32)]
    public string? ObservationValueAbsentReason { get; init; }

    [HL7Field(33)]
    public string? ObservationRelatedSpecimenIdentifier { get; init; }
}