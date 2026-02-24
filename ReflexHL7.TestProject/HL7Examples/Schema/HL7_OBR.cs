using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7SegmentDefinition("OBR")]
[ExcludeFromCodeCoverage]
public partial class HL7_OBR
{
    [HL7Field(1)]
    public string? SetId { get; init; }

    [HL7Field(2)]
    public string? PlacerOrderNumber { get; init; }

    [HL7Field(3)]
    public string? FillerOrderNumber { get; init; }

    [HL7Field(4)]
    public required string[] UniversalServiceIdentifier { get; init; }

#if INCLUDE__SHOULD_NOT
    [HL7Field(5)]
    public string? Priority { get; init; }

    [HL7Field(6)]
    public required HL7_DTM RequestedDateTime { get; init; }
#endif

    [HL7Field(7)]
    public HL7_DTM? ObservationDateTime { get; init; }

    [HL7Field(8)]
    public HL7_DTM? ObservationEndDateTime { get; init; }

    [HL7Field(9)]
    public string? CollectionVolume { get; init; }

    [HL7Field(10)]
    public string? CollectorIdentifier { get; init; }

    [HL7Field(11)]
    public string? SpecimenActionCode { get; init; }

    [HL7Field(12)]
    public string? DangerCode { get; init; }

    [HL7Field(13)]
    public HL7String? RelevantClinicalInformation { get; init; }

#if INCLUDE__SHOULD_NOT
    [HL7Field(14)]
    public HL7_DTM? SpecimenReceivedDateTime { get; init; }

    [HL7Field(15)]
    public string? SpecimenSource { get; init; }
#endif

    [HL7Field(16)]
    public string? OrderingProvider { get; init; }

    [HL7Field(17)]
    public string? OrderCallbackPhoneNumber { get; init; }

    [HL7Field(18)]
    public string? PlacerField1 { get; init; }

    [HL7Field(19)]
    public string? PlacerField2 { get; init; }

    [HL7Field(20)]
    public string? FillerField1 { get; init; }

    [HL7Field(21)]
    public string? FillerField2 { get; init; }

    [HL7Field(22)]
    public HL7_DTM? ResultsRepeatStatusChangeDateTime { get; init; }

    [HL7Field(23)]
    public string? ChargeToPractice { get; init; }

    [HL7Field(24)]
    public string? DiagnosticServSectId { get; init; }

    [HL7Field(25)]
    public string? ResultStatus { get; init; }

    [HL7Field(26)]
    public string? ParentResult { get; init; }

#if INCLUDE__SHOULD_NOT
    [HL7Field(27)]
    public string? QuantityTiming { get; init; }
#endif

    [HL7Field(28)]
    public string? ResultCopiesTo { get; init; }

    [HL7Field(29)]
    public string? ParentResultsObservationIdentifier { get; init; }

    [HL7Field(30)]
    public string? TransportationMode { get; init; }

    [HL7Field(31)]
    public string? ReasonForStudy { get; init; }

    [HL7Field(32)]
    public string? PrincipalResultInterpreter { get; init; }

    [HL7Field(33)]
    public string? AssistantResultInterpreter { get; init; }

    [HL7Field(34)]
    public string? Technician { get; init; }

    [HL7Field(35)]
    public string? Transcriptionist { get; init; }

    [HL7Field(36)]
    public HL7_DTM? ScheduledDateTime { get; init; }

    [HL7Field(37)]
    public string? NumberOfSampleContainers { get; init; }

    [HL7Field(38)]
    public string? TransportLogisticsOfCollectedSample { get; init; }

    [HL7Field(39)]
    public string? CollectorComment { get; init; }

    [HL7Field(40)]
    public string? TransportArrangementResponsibility { get; init; }

    [HL7Field(41)]
    public string? TransportArranged { get; init; }

    [HL7Field(42)]
    public string? EscortRequired { get; init; }

    [HL7Field(43)]
    public string? PlannedPatientTransportComment { get; init; }

    [HL7Field(44)]
    public string? ProcedureCode { get; init; }

    [HL7Field(45)]
    public string? ProcedureCodeModifier { get; init; }

    [HL7Field(46)]
    public string? PlacerSupplementalServiceInformation { get; init; }

    [HL7Field(47)]
    public string? FillerSupplementalServiceInformation { get; init; }

    [HL7Field(48)]
    public string? MedicallyNecessaryDuplicateProcedureReason { get; init; }

    [HL7Field(49)]
    public string? ResultHandling { get; init; }

    [HL7Field(50)]
    public string? ParentUniversalServiceIdentifier { get; init; }

    [HL7Field(51)]
    public string? ObservationGroupID { get; init; }

    [HL7Field(52)]
    public string? ParentObservationGroupID { get; init; }

    [HL7Field(53)]
    public string? AlternatePlacerOrderNumber { get; init; }

    [HL7Field(54)]
    public string? ParentOrder { get; init; }

    [HL7Field(55)]
    public string? ActionCode { get; init; }
}