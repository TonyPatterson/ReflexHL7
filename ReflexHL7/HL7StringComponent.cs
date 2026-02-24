using System.Text.Json.Serialization;

namespace ReflexHL7;

public class HL7StringComponent
{
    public HL7StringComponentType Type { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Content { get; }

    public HL7StringComponent(HL7StringComponentType type, string? content = null)
    {
        Type = type;
        Content = content;
    }
}