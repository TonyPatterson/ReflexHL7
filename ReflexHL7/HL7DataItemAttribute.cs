using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[ExcludeFromCodeCoverage]
public class HL7DataItemAttribute : Attribute
{
    public bool IsCollection { get; set; }
}