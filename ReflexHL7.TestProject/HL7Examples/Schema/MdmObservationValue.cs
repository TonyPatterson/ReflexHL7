using ReflexHL7;
using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

[HL7FieldDefinition]
[ExcludeFromCodeCoverage]
public partial class MdmObservationValue
{
    [HL7Component(3)]
    public string? Type { get; private set; }

    [HL7Component(4)]
    public string? Format { get; private set; }

    [HL7Component(5)]
    public byte[]? Content { get; private set; }
}