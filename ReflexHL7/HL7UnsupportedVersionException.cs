using System.Diagnostics.CodeAnalysis;

namespace ReflexHL7;

[Serializable]
[ExcludeFromCodeCoverage]
public class HL7UnsupportedVersionException : Exception
{
    public HL7UnsupportedVersionException()
    {
    }

    public HL7UnsupportedVersionException(string? message)
        : base(message)
    {
    }

    public HL7UnsupportedVersionException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}