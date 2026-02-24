using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
[ExcludeFromCodeCoverage]
public class HL7FieldAttribute(int fieldIndex) : HL7DataItemAttribute
{
    public int FieldIndex { get; set; } = fieldIndex;
}