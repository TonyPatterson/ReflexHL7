using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace ReflexHL7.CodeGenerator;

[DebuggerDisplay("MessagePropertyToGenerate: {Name,nq} : {PropertyType}")]
internal class MessagePropertyToGenerate
{
    public string Name { get; }

    public string? SkipSegments { get; }

    public string PropertyType { get; }

    public bool IsCollection { get; }

    public bool IsHL7Collection { get; }

    public MessagePropertyToGenerate(
        string name,
        ITypeSymbol propertyType,
        string? skipSegments,
        bool isHL7Collection)
    {
        Name = name;
        SkipSegments = skipSegments;
        IsHL7Collection = isHL7Collection;

        string propertyTypeName = GetQualifiedTypeName(propertyType);

        IsCollection = propertyTypeName == "System.Collections.Generic.IReadOnlyList";

        if (IsCollection)
        {
            var nts = propertyType as INamedTypeSymbol;

            PropertyType = GetQualifiedTypeName(nts!.TypeArguments[0]);
        }
        else
        {
            PropertyType = propertyTypeName;
        }

        static string GetQualifiedTypeName(ITypeSymbol t) =>
            $"{t.ContainingNamespace}.{t.Name}";
    }
}