using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
[ExcludeFromCodeCoverage]
public class HL7SegmentAttribute(string segmentName) : Attribute
{
    public string SegmentName { get; set; } = segmentName;
}