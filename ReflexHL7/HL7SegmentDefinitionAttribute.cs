using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
[ExcludeFromCodeCoverage]
public class HL7SegmentDefinitionAttribute(string segmentName) : Attribute
{
    public string SegmentName { get; } = segmentName;
}