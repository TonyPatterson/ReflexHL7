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
                if (propertyType is IArrayTypeSymbol ats)
                    return ats.ElementType.ToString();

                if (propertyType is INamedTypeSymbol nts && nts.ConstructedFrom.ToString() == "System.Collections.Generic.IReadOnlyList<T>")
                    return nts.TypeArguments[0].ToString();
            }

            return propertyType.ToString();
        }
    }

    int IComparable<PropertyToGenerate>.CompareTo(PropertyToGenerate other)
        => Index.CompareTo(other.Index);
}