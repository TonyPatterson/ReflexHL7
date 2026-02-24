using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace ReflexHL7.CodeGenerator;

internal abstract class HL7CodeGenerationCharacteristics(string containerAttribute)
{
    internal string ContainerAttribute { get; } = containerAttribute;

    public abstract TargetToGenerate? GetClassToGenerate(
        SemanticModel semanticModel,
        SyntaxNode syntaxNode,
        ImmutableArray<TypedConstant> constructorArguments);
}