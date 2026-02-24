using System.Collections.Generic;

namespace ReflexHL7.Tests;

public class HL7StringTests
{
    [Fact]
    public void HL7String_EmptyString()
    {
        var str = new StringReader(
            """
            MSH|^~\&|SA|SF|NHS|PAS|||MDM^T02|1143|P|2.4.1|||NE|AL|GBR|UTF-8|EN|
            EVN||20210801181017|
            """);

        var reader = new HL7Tokeniser(str);

        var s = new HL7String(reader, string.Empty);

        Assert.Equal(string.Empty, s.ToString());
    }

    [Fact]
    public void HL7String_StringWithSeparators()
    {
        var str = new StringReader(
            """
            MSH|^~\&|SA|SF|NHS|PAS|||MDM^T02|1143|P|2.4.1|||NE|AL|GBR|UTF-8|EN|
            EVN||20210801181017|
            """);

        var reader = new HL7Tokeniser(str);

        var s = new HL7String(reader, @"Text with HL7 Separa\T\or charact\E\\R\s e\S\caped \F\or testing");

        Assert.Equal(@"Text with HL7 Separa&or charact\~s e^caped |or testing", s.ToString());
    }

    [Fact]
    public void HL7String_StringWithComplexEscapes()
    {
        var str = new StringReader(
            """
            MSH|^~\&|SA|SF|NHS|PAS|||MDM^T02|1143|P|2.4.1|||NE|AL|GBR|UTF-8|EN|
            EVN||20210801181017|
            """);

        var reader = new HL7Tokeniser(str);

        var hl7String = new HL7String(reader, @"Text with \.br\ Line breaks and fill mode \.fi\ And also \S\\F\ simple escapes and \.in\\.ti\ adjacent escapes\P\");

        Assert.Equal("Text with  Line breaks and fill mode  And also ^| simple escapes and  adjacent escapes", hl7String.ToString());

        Assert.Equal(9, hl7String.Count);

        VerifyContentItem(0, HL7StringComponentType.Text, "Text with ");
        VerifyContentItem(1, HL7StringComponentType.Formatting, ".br");
        VerifyContentItem(2, HL7StringComponentType.Text, " Line breaks and fill mode ");
        VerifyContentItem(3, HL7StringComponentType.Formatting, ".fi");
        VerifyContentItem(4, HL7StringComponentType.Text, " And also ^| simple escapes and ");
        VerifyContentItem(5, HL7StringComponentType.Formatting, ".in");
        VerifyContentItem(6, HL7StringComponentType.Formatting, ".ti");
        VerifyContentItem(7, HL7StringComponentType.Text, " adjacent escapes");
        VerifyContentItem(8, HL7StringComponentType.Truncation);

        void VerifyContentItem(int index, HL7StringComponentType type, string? content = null)
        {
            var item = hl7String[index];

            Assert.Equal(type, item.Type);
            Assert.Equal(content, item.Content);
        }
    }

    [Fact]
    public void HL7String_NonCanonicalCharacterChoices()
    {
        // TODO: Does not test sub-component or repetition separators.
        var str = new StringReader(
            """
            MSH:#@%?:SA:SF:NHS:PAS:::MDM#T02:1143:P:2.4.1:::NE:AL:GBR:UTF-8:EN:
            EVN::20210801181017:
            """);

        var reader = new HL7Tokeniser(str);

        // Extracting MSH-9 requires field and sub-component separators to be properly handled
        Assert.Equal("MDM", reader.MshRecord.MessageType[0]);
        Assert.Equal("T02", reader.MshRecord.MessageType[1]);

        // String handling requires escape character to be properly handled
        var hl7String = new HL7String(reader, @"Text with %.br% Line breaks and fill mode %.fi% And also %S%%F% simple escapes and %.in%%.ti% adjacent escapes%P%");

        Assert.Equal("Text with  Line breaks and fill mode  And also #: simple escapes and  adjacent escapes", hl7String.ToString());
    }
}