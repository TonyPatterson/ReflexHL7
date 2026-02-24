using System.Text.Json.Serialization;

namespace ReflexHL7;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HL7StringComponentType
{
    Text,
    Formatting,
    Highlight,
    Normal,
    Truncation,
    Hexadecimal,
    LocallyDefined,
    SingleByteCharacterSet,
    MultipleByteCharacterSet
}