using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

/// <summary>
/// Flags an argument as one which maps to a sub-component within a
/// component.
/// </summary>
/// <param name="subComponentIndex">The 1-based index of the sub-component
/// within the component.</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
[ExcludeFromCodeCoverage]
public class HL7SubComponentAttribute(int subComponentIndex) : HL7DataItemAttribute
{
    /// <summary>
    /// The 1-based index of the sub-component within the component.
    /// </summary>
    public int SubComponentIndex { get; set; } = subComponentIndex;
}