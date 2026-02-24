using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;

namespace ReflexHL7.CodeGenerator;

[DebuggerDisplay("ClassToGenerate: {Namespace,nq}.{Name,nq}")]
internal record ClassToGenerate(
    string Name,
    string NamespaceName,
    HL7CodeGenerationCharacteristics_SegmentAndLower Characteristics,
    IReadOnlyList<PropertyToGenerate> Properties,
    ImmutableArray<TypedConstant> ConstructorArguments) : TargetToGenerate(Name, NamespaceName)
{
    public override string GenerateReadMethod()
    {
        var csb2 = new CodeStringBuilder();

        csb2.Indent(3);

        var csb = WritePrologue();

        if (Characteristics.SegmentNameCheck)
        {
            csb.AppendLine($"if (tokeniser.CurrentSegmentName != \"{ConstructorArguments[0].Value}\")");
            csb.Indent();
            csb.AppendLine("return null;");
            csb.Unindent();
            csb.AppendLine();
        }

        int propertyIndex = 0;

        foreach (var prop in Properties)
        {
            while (++propertyIndex < prop.Index)
                csb.AppendLine($"tokeniser.{Characteristics.SkipThis}();");

            csb2.AppendLine($"{prop.Name} = temp{prop.Name},");

            string assign = $"var temp{prop.Name} = ";

            if (prop.IsFieldMappedToCollection)
            {
                AddCollectionRead(csb, prop, assign);
            }
            else
            {
                AddSingleRead(csb, prop, assign);
            }
        }

        csb.AppendLine();
        csb.AppendLine($"tokeniser.{Characteristics.SkipContainer}();");
        csb.AppendLine();
        csb.AppendLine($"return new()");
        csb.AppendLine("{");
        csb.Append(csb2);
        csb.AppendLine("};");
        csb.Unindent();
        csb.AppendLine("}");

        csb.AppendLine();

        csb.AppendLine("/// <summary>");
        csb.AppendLine("/// Read repeated HL7 V2 objects from the supplied token stream.");
        csb.AppendLine("/// </summary>");
        csb.AppendLine("/// <param name=\"tokeniser\">The tokeniser to read the object from.");
        csb.AppendLine("/// </param>");
        csb.AppendLine("/// <returns>The parsed HL7 V2 object collection.</returns>");
        csb.AppendLine("[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]");
        csb.AppendLine($"public static System.Collections.Generic.IEnumerable<{Name}> ReadMultiple(ReflexHL7.HL7Tokeniser tokeniser)");
        csb.AppendLine("{");
        csb.Indent();
        csb.AppendLine("while (true)");
        csb.AppendLine("{");
        csb.Indent();
        csb.AppendLine("var segment = Read(tokeniser);");
        csb.AppendLine();
        csb.AppendLine("if (segment is null)");
        csb.Indent();
        csb.AppendLine("break;");
        csb.Unindent();
        csb.AppendLine();
        csb.AppendLine("yield return segment;");
        csb.Unindent();
        csb.AppendLine("}");
        WriteEpilogue(csb);

        return csb.ToString();
    }

    private void AddSingleRead(CodeStringBuilder csb, PropertyToGenerate prop, string assign)
    {
        if (prop.BasePropertyType.IndexOf("IReadOnlyList") >= 0)
            throw new NotSupportedException("IReadOnlyList is not yet supported as a property type.");

        switch (prop.BasePropertyType)
        {
            case "byte[]":
            case "byte[]?":
                csb.AppendLine($"{assign}Convert.FromBase64String(tokeniser.{Characteristics.ReadThis}());");
                break;

            case "string?[]?":
            case "string[]?":
            case "string?[]":
            case "string[]":
                csb.AppendLine($"{assign}tokeniser.{Characteristics.ReadSubs}().ToArray();");
                break;

            case "ReflexHL7.HL7_DTM?":
            case "ReflexHL7.HL7_DTM":
                csb.AppendLine($"{assign}ReflexHL7.HL7_DTM.Read(tokeniser.{Characteristics.ReadThis}());");
                break;

            case "ReflexHL7.HL7String?":
            case "ReflexHL7.HL7String":
                csb.AppendLine($"{assign}new ReflexHL7.HL7String(tokeniser, tokeniser.{Characteristics.ReadThis}());");
                break;

            case "ReflexHL7.HL7String[]?":
            case "ReflexHL7.HL7String?[]":
            case "ReflexHL7.HL7String?[]?":
            case "ReflexHL7.HL7String[]":
                throw new NotSupportedException("Collections of ReflexHL7.HL7String are not yet supported.");

            case "string":
            case "string?":
                csb.AppendLine($"{assign}tokeniser.{Characteristics.ReadThis}();");
                break;

            default:
                csb.AppendLine($"{assign}{prop.BasePropertyType.Replace("?", string.Empty)}.Read(tokeniser);");
                break;
        }
    }

    private void AddCollectionRead(CodeStringBuilder csb, PropertyToGenerate prop, string assign)
    {
        csb.AppendLine($"{assign}tokeniser.ReadRepetitions(t =>");
        csb.AppendLine("{");
        csb.Indent();
        AddSingleRead(csb, prop, "return ");
        csb.Unindent();
        csb.AppendLine("}).ToArray();");
    }
}