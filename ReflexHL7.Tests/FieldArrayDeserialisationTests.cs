using System.Collections.Generic;

namespace ReflexHL7.Tests;

[HL7SegmentDefinition("XXX")]
public partial class SegmentArrayTest
{
    [HL7Field(1)]
    public required int[] IntArray { get; set; }

    [HL7Field(2)]
    public required IReadOnlyList<float> FloatReadOnlyList { get; set; }

    [HL7Field(3)]
    public required string[] StringArray { get; set; }

    [HL7Field(4)]
    public required string?[] NullableStringArray { get; set; }

    [HL7Field(5)]
    public required IReadOnlyList<string> StringList { get; set; }

    [HL7Field(6)]
    public required IReadOnlyList<string?> NullableStringList { get; set; }
}

public class FieldArrayDeserialisationTests
{
    private readonly SegmentArrayTest _segmentArrayTest;

    public FieldArrayDeserialisationTests()
    {
        const string source = """
            MSH|^~\&|||||||MDM^T02|A||2.4|
            XXX|2602^1971|3.14^1.62^2.72|md^sl|es^""^hm|abc^def|ghi^^jkl^""|
            """;

        var reader = new StringReader(source);

        var tokeniser = new HL7Tokeniser(reader);

        _segmentArrayTest = SegmentArrayTest.Read(tokeniser);
    }

    [Fact]
    public void IntegerArray()
    {
        Assert.Equal([2602, 1971], _segmentArrayTest.IntArray);
    }

    [Fact]
    public void FloatArray()
    {
        Assert.Equal([(float)3.14, (float)1.62, (float)2.72], _segmentArrayTest.FloatReadOnlyList);
    }

    [Fact]
    public void StringArray()
    {
        Assert.Equal<string[]>(["md", "sl"], _segmentArrayTest.StringArray);
    }

    [Fact]
    public void NullableStringArray()
    {
        Assert.Equal<string?[]>(["es", HL7Tokeniser.PresentButNull, "hm"], _segmentArrayTest.NullableStringArray);
    }

    [Fact]
    public void StringList()
    {
        Assert.Equal(["abc", "def"], _segmentArrayTest.StringList);
    }

    [Fact]
    public void NullableStringList()
    {
        Assert.Equal(["ghi", "", "jkl", HL7Tokeniser.PresentButNull], _segmentArrayTest.NullableStringList);
    }
}
