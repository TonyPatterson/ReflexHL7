using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace ReflexHL7.CodeGenerator;

internal class HL7CodeGenerationCharacteristics_SegmentAndLower(
    string containerAttribute,
    string readThis,
    string skipThis,
    string? readSubs,
    string skipContainer,
    string propertyAttribute,
    bool segmentNameCheck = false) : HL7CodeGenerationCharacteristics(containerAttribute)
{
    internal string ReadThis { get; } = readThis;

    internal string SkipThis { get; } = skipThis;

    internal string? ReadSubs { get; } = readSubs;

    internal string SkipContainer { get; } = skipContainer;

    internal string PropertyAttribute { get; } = propertyAttribute;

    internal bool SegmentNameCheck { get; } = segmentNameCheck;

    public override TargetToGenerate? GetClassToGenerate(
        SemanticModel semanticModel,
        SyntaxNode syntaxNode,
        ImmutableArray<TypedConstant> constructorArguments)
    {
        if (semanticModel.GetDeclaredSymbol(syntaxNode) is not INamedTypeSymbol symbol)
            return null;

        var discoveredMembers = symbol.GetMembers();

        var members = new List<PropertyToGenerate>(discoveredMembers.Length);

        foreach (ISymbol member in discoveredMembers)
        {
            if (member is IPropertySymbol prop)
            {
                var attr = GetAttributeData(PropertyAttribute, member);
                bool isCollection = false;
                int index = -1;

                if (attr is not null)
                {
                    index = (int)attr.ConstructorArguments[0].Value!;

                    foreach (var na in attr.NamedArguments)
                    {
                        switch (na.Key)
                        {
                            case "IsCollection":
                                isCollection = (bool)na.Value.Value!;
                                break;
                        }
                    }
                }

                if (index >= 0)
                    members.Add(new PropertyToGenerate(prop.Name, prop.Type, index, isCollection));
            }
        }

        members.Sort();

        return new ClassToGenerate(
            symbol.Name,
            symbol.ContainingNamespace.ToString(),
            this,
            members,
            constructorArguments);
    }

    private static AttributeData? GetAttributeData(string attributeName, ISymbol member)
    {
        foreach (var attr in member.GetAttributes())
        {
            if (attr.AttributeClass!.ToString() == attributeName)
                return attr;
        }

        return null;
    }
}