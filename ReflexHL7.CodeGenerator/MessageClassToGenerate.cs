using System.Diagnostics;

namespace ReflexHL7.CodeGenerator;

[DebuggerDisplay("ClassToGenerate: {Namespace,nq}.{Name,nq}")]
internal record MessageClassToGenerate(
    string Name,
    string NamespaceName,
    HL7CodeGenerationCharacteristics_Message Characteristics,
    IReadOnlyList<MessagePropertyToGenerate> Properties,
    string? AllowedVersions,
    string? TrailingSegments) : TargetToGenerate(Name, NamespaceName)
{
    private const string MshTypeName = "ReflexHL7.HL7_MSH";

    public override string GenerateReadMethod()
    {
        Debug.WriteLine($"Generating output for {NamespaceName}.{Name}");

        var csb = WritePrologue();

        if (AllowedVersions is not null)
        {
            var q = from v in AllowedVersions.Split('|')
                    select v.Trim();

            string joiner = "\", \"";

            csb.AppendLine($"tokeniser.VerifyVersion(\"{string.Join(joiner, q)}\");");

            csb.AppendLine();
        }

        foreach (var prop in Properties)
        {
            AddSkipSegmentCalls(csb, prop.SkipSegments);

            string tn = prop.PropertyType;
            string pn = prop.Name;

            if (tn == MshTypeName)
                continue;

            string method = prop.IsCollection ? "ReadMultiple" : "Read";
            string toArray = prop.IsCollection ? ".ToArray()" : string.Empty;

            csb.AppendLine($"var {pn} = {tn}.{method}(tokeniser){toArray};");
        }

        AddSkipSegmentCalls(csb, TrailingSegments);

        csb.AppendLine();
        csb.AppendLine($"return new {NamespaceName}.{Name}");
        csb.AppendLine("{");
        csb.Indent();

        foreach (var prop in Properties)
        {
            string tn = prop.PropertyType;
            string pn = prop.Name;

            if (tn == MshTypeName)
            {
                csb.AppendLine($"{pn} = tokeniser.MshRecord,");
            }
            else
            {
                csb.AppendLine($"{pn} = {pn},");
            }
        }

        csb.Unindent();
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
        csb.AppendLine("int startingStreamPosition = tokeniser.StreamPosition;");
        csb.AppendLine();
        csb.AppendLine("var item = Read(tokeniser);");
        csb.AppendLine();
        csb.AppendLine("if (tokeniser.StreamPosition == startingStreamPosition)");
        csb.Indent();
        csb.AppendLine("break;");
        csb.Unindent();
        csb.AppendLine();
        csb.AppendLine("yield return item;");
        csb.Unindent();
        csb.AppendLine("}");
        csb.Unindent();
        csb.AppendLine("}");
        csb.AppendLine();

        csb.AppendLine("/// <summary>");
        csb.AppendLine("/// Read the HL7 V2 object from the supplied token stream.");
        csb.AppendLine("/// </summary>");
        csb.AppendLine("/// <param name=\"s\">The stream to read the message from.");
        csb.AppendLine("/// </param>");
        csb.AppendLine("/// <returns>The parsed HL7 V2 object or null if not available.</returns>");
        csb.AppendLine($"public static {NamespaceName}.{Name} Read(System.IO.TextReader s)");
        csb.AppendLine("{");
        csb.Indent();
        csb.AppendLine("return Read(new ReflexHL7.HL7Tokeniser(s));");

        WriteEpilogue(csb);

        return csb.ToString();

        static void AddSkipSegmentCalls(CodeStringBuilder csb, string? propertyValue)
        {
            if (propertyValue is null)
                return;

            foreach (var skipSegment in propertyValue.Split(','))
                csb.AppendLine($"tokeniser.SkipSegments(\"{skipSegment.Trim()}\");");
        }
    }
}