using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

/// <summary>
/// In a message or a group, identifies a list of segments to skip over
/// before the next member is read. This allows the class to ignore
/// segments and not map them to class members.
/// </summary>
/// <param name="segmentNameList">A comma-separated list of segment
/// names to ignore. The ordering is important as the parser will
/// locate the next read segment in the list, skip it and move along the
/// list until the list is exhausted.</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
[ExcludeFromCodeCoverage]
public class HL7SkipSegmentsAttribute(string segmentNameList) : Attribute
{
    /// <summary>
    /// The 1-based index of the sub-component within the component.
    /// </summary>
    public string SegmentNameList { get; set; } = segmentNameList;
}