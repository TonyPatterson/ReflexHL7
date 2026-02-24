using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

/// <summary>
/// Represents the HL7 HD structure for system identification.
/// </summary>
[HL7ComponentDefinition]
[ExcludeFromCodeCoverage]
public partial class HL7_HD_Component
{
    /// <summary>
    /// Gets the namespace ID of the system.
    /// </summary>
    [HL7SubComponent(1)]
    public string? NamespaceId { get; private init; }

    /// <summary>
    /// Gets the universal ID of the system.
    /// </summary>
    [HL7SubComponent(2)]
    public string? UniversalId { get; private init; }

    /// <summary>
    /// Gets the universal ID type of the system.
    /// </summary>
    [HL7SubComponent(3)]
    public string? UniversalIdType { get; private init; }
}