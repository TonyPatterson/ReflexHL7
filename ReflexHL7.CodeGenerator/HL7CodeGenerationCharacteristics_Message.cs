using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace ReflexHL7.CodeGenerator;

internal class HL7CodeGenerationCharacteristics_Message(
    string containerAttribute) : HL7CodeGenerationCharacteristics(containerAttribute)
{
    public override TargetToGenerate? GetClassToGenerate(
        SemanticModel semanticModel,
        SyntaxNode syntaxNode,
        ImmutableArray<TypedConstant> constructorArguments)
    {
        if (semanticModel.GetDeclaredSymbol(syntaxNode) is not INamedTypeSymbol symbol)
            return null;

        var messageDefinitionAttribute = GetHL7MessageDefinitionAttribute(symbol);

        string? allowedVersions = GetAllowedVersions(messageDefinitionAttribute);
        string? trailingSegments = GetTrailingSegments(messageDefinitionAttribute);

        var discoveredMembers = symbol.GetMembers();

        var members = new List<MessagePropertyToGenerate>(discoveredMembers.Length);

        foreach (ISymbol member in discoveredMembers)
        {
            if (member is IPropertySymbol property)
            {
                string? skipSegments = GetSkipSegments(member);

                members.Add(new MessagePropertyToGenerate(property.Name, property.Type, skipSegments, false));
            }
        }

        return new MessageClassToGenerate(
            symbol.Name,
            symbol.ContainingNamespace.ToString(),
            this,
            members,
            allowedVersions,
            trailingSegments);
    }

    private static string? GetMessageDefinitionAttribute(AttributeData? messageDefinitionAttribute, string propertyName)
    {
        if (messageDefinitionAttribute is null)
            return null;

        var q = from arg in messageDefinitionAttribute.NamedArguments
                where arg.Key == propertyName
                select arg.Value.Value as string;

        return q.SingleOrDefault();
    }

    private static string? GetAllowedVersions(AttributeData? messageDefinitionAttribute) =>
        GetMessageDefinitionAttribute(messageDefinitionAttribute, "AllowVersions");

    private static string? GetTrailingSegments(AttributeData? messageDefinitionAttribute) =>
        GetMessageDefinitionAttribute(messageDefinitionAttribute, "TrailingSegments");

    private static string? GetSkipSegments(ISymbol member)
    {
        var attr = GetAttributeByName(member, "ReflexHL7.HL7SkipSegmentsAttribute");

        if (attr is null)
            return null;

        return (string)attr.ConstructorArguments[0].Value!;
    }

    private static AttributeData? GetHL7MessageDefinitionAttribute(ISymbol member) =>
        GetAttributeByName(member, "ReflexHL7.HL7MessageDefinitionAttribute");

    private static AttributeData? GetAttributeByName(
        ISymbol member,
        string attributeFullName)
    {
        foreach (var attr in member.GetAttributes())
        {
            if (attr.AttributeClass!.ToString() == attributeFullName)
                return attr;
        }

        return null;
    }
}