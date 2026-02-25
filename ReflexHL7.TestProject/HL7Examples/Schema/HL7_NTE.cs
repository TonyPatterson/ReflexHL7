using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7SegmentDefinition("NTE")]
[ExcludeFromCodeCoverage]
public partial class HL7_NTE
{
    [HL7Field(1)]
    public int SetId { get; init; }

    // TODO: test as different types : IsCollection/false or true as a simple string or IROL<string>
    // all permutations possible for documentation
    // TODO: test arrays of complex types as well, e.g. IROL<HL7_XON> or HL7_XON[] or IROL<string>[] etc.

    [HL7Field(3, IsCollection = true)]
    public required string[] Comment { get; init; }
}