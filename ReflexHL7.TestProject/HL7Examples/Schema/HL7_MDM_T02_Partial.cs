using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

/// <summary>
/// A lightweight version of MDM_T02 which reads the first OBX only.
/// </summary>
[HL7MessageDefinition(AllowVersions = "2.4")]
[ExcludeFromCodeCoverage]
public partial class HL7_MDM_T02_Partial
{
    [HL7Segment("MSH")]
    [HL7SkipSegments("ARV,SFT,UAC,EVN,PID,PRT,PV1,PRT,ORC,PRT,TQ1,TQ2,OBR,PRT,NTE,TXA,CON")]
    public required HL7_MSH MSH { get; init; }

    [HL7SkipSegments("TXA,CON")]
    public required HL7_MDM_OBX OBX { get; init; }
}