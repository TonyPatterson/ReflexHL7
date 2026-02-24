using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
[ExcludeFromCodeCoverage]
public class HL7ComponentAttribute(int componentIndex) : HL7DataItemAttribute
{
    public int ComponentIndex { get; set; } = componentIndex;
}