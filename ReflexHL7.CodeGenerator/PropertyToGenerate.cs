using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace ReflexHL7.CodeGenerator;

[DebuggerDisplay("PropertyToGenerate: {Name,nq}({Index}) : {PropertyType}")]
internal class PropertyToGenerate(
    string name,
    ITypeSymbol propertyType,
    int index,
    bool isFieldMappedToCollection) : IComparable<PropertyToGenerate>
{
    public string Name { get; } = name;

    public string PropertyType { get; } = propertyType.ToString();

    public int Index { get; } = index;

    public bool IsFieldMappedToCollection { get; } = isFieldMappedToCollection;

    public string BasePropertyType
    {
        get
        {
            if (IsFieldMappedToCollection)
            {
                // The HL7 Field attribute IsCollection has been used
                string s = propertyType.ToString();

                if (s.EndsWith("?"))
                    s = s.Substring(0, s.Length - 1);

                if (s.EndsWith("[]"))
                    s = s.Substring(0, s.Length - 2);

                return s;
            }

            return propertyType.ToString();
        }
    }

    int IComparable<PropertyToGenerate>.CompareTo(PropertyToGenerate other)
        => Index.CompareTo(other.Index);
}