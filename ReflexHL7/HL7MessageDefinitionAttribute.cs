using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
[ExcludeFromCodeCoverage]
public class HL7MessageDefinitionAttribute : Attribute
{
    /// <summary>
    /// Specifies version numbers that the parsed message's version field should match. If not
    /// supplied, no version number check is done. If supplied, the version should match one of the
    /// specified versions exactly, with no superfluous characters. If it doesn't, the
    /// HL7UnsupportedVersionException will be thrown. The '|' character is used as a separator
    /// between multiple versions. e.g. (AllowVersions = "2.3.1|2.5.1").
    /// </summary>
    public string? AllowVersions { get; set; }

    /// <summary>
    /// Provides a list of segments that should appear at the end of the message, but which are
    /// not mapped to properties. This is necessary if the tokeniser needs to read a subsequent
    /// block of segments, to ensure that it correctly skips past unmapped content in the current
    /// item. The segments should be listed in order, separated by commas.
    /// </summary>
    public string? TrailingSegments { get; set; }
}