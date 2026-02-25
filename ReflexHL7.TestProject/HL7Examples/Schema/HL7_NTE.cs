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
    // TODO: Replace HL7 string with user-defined type and interpreter,
    // The interpreter gets passed the info from the HL7 string, but
    // is allowed to interpret it as it sees fit, loading the target class.
    // For example, if the target class is string, the interpreter could convert
    // \H\ and \N\ to <bold> and </bold>. The interface would be called
    // IHL7FormattedTextDeserialiser and would use an attribute like [FormattedTextDeserialiser(typeof(MyCustomDeserialiser))] on the target class or field.
    // Probably need to distinguish between datatype ST (which won't include formatting, but can have other escapes)
    // and datatype FT (which includes formatting). Maybe the attribute is only needed on the FT case,
    // or maybe the deserialiser needs to be able to handle both cases.
    [HL7Field(3, IsCollection = true)]
    public required string[] Comment { get; init; }
}