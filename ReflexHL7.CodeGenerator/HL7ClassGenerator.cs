using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace ReflexHL7.CodeGenerator;

[Generator]
public class HL7ClassGenerator : IIncrementalGenerator
{
    private static readonly HL7CodeGenerationCharacteristics_SegmentAndLower SegmentGenerator = new(
        containerAttribute: "ReflexHL7.HL7SegmentDefinitionAttribute",
        readThis: "ReadField",
        skipThis: "SkipField",
        skipContainer: "SkipSegment",
        readSubs: "ReadComponents",
        propertyAttribute: "ReflexHL7.HL7FieldAttribute",
        segmentNameCheck: true);

    private static readonly HL7CodeGenerationCharacteristics_SegmentAndLower FieldGenerator = new(
        containerAttribute: "ReflexHL7.HL7FieldDefinitionAttribute",
        readThis: "ReadComponent",
        skipThis: "SkipComponent",
        skipContainer: "SkipField",
        readSubs: "ReadSubComponents",
        propertyAttribute: "ReflexHL7.HL7ComponentAttribute");

    private static readonly HL7CodeGenerationCharacteristics_SegmentAndLower ComponentGenerator = new(
        containerAttribute: "ReflexHL7.HL7ComponentDefinitionAttribute",
        readThis: "ReadSubComponent",
        skipThis: "SkipSubComponent",
        skipContainer: "SkipComponent",
        readSubs: "# Error #",
        propertyAttribute: "ReflexHL7.HL7SubComponentAttribute");

    private static readonly HL7CodeGenerationCharacteristics_Message MessageGenerator = new(
        containerAttribute: "ReflexHL7.HL7MessageDefinitionAttribute");

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        GetAllClassesToGenerate(context, SegmentGenerator);
        GetAllClassesToGenerate(context, FieldGenerator);
        GetAllClassesToGenerate(context, ComponentGenerator);
        GetAllClassesToGenerate(context, MessageGenerator);
    }

    private static void GetAllClassesToGenerate(
        IncrementalGeneratorInitializationContext context,
        HL7CodeGenerationCharacteristics generator)
    {
        var classesToGenerate = context.SyntaxProvider.ForAttributeWithMetadataName(
            generator.ContainerAttribute,
            predicate: static (s, _) => true,
            transform: (ctx, _) => generator.GetClassToGenerate(ctx.SemanticModel, ctx.TargetNode, ctx.Attributes[0].ConstructorArguments))
            .Where(static m => m is not null);

        context.RegisterSourceOutput(
            classesToGenerate,
            static (spc, source) => Execute(source, spc));
    }

    private static void Execute(TargetToGenerate? targetToGenerate, SourceProductionContext context)
    {
        if (targetToGenerate is null)
            return;

        System.Diagnostics.Debug.WriteLine(targetToGenerate);

        string result = targetToGenerate.GenerateReadMethod();

        context.AddSource(
            $"ReflexHL7.GeneratedReader.{targetToGenerate.Name}.g.cs",
            SourceText.From(result, Encoding.UTF8));
    }
}