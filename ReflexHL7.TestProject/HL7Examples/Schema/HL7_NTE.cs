using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7SegmentDefinition("NTE")]
[ExcludeFromCodeCoverage]
public partial class HL7_NTE
{
    [HL7Field(1)]
    public required string SetId { get; init; }

    [HL7Field(3, IsCollection = true)]
    public required string[] Comment { get; init; }
}