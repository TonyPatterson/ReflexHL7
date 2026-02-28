using System.Collections.Generic;

namespace ReflexHL7.Tests;

[HL7SegmentDefinition("XXX")]
public partial class SegmentRepeatingTest
{
    [HL7Field(1, IsCollection = true)]
    public required int[] IntArray { get; set; }

    [HL7Field(2, IsCollection = true)]
    public required IReadOnlyList<float> FloatReadOnlyList { get; set; }

    [HL7Field(3, IsCollection = true)]
    public required string[] StringArray { get; set; }

    [HL7Field(4, IsCollection = true)]
    public required string?[] NullableStringArray { get; set; }

    [HL7Field(5, IsCollection = true)]
    public required IReadOnlyList<string> StringList { get; set; }

    [HL7Field(6, IsCollection = true)]
    public required IReadOnlyList<string?> NullableStringList { get; set; }
}

public class FieldRepeatingTests
{
    private readonly SegmentRepeatingTest _segmentRepeatingTest;

    public FieldRepeatingTests()
    {
        const string source = """
            MSH|^~\&|||||||MDM^T02|A||2.4|
            XXX|2602~1971|3.14~1.62~2.72|md~sl|es~""~hm|abc~def|ghi~~jkl~""|
            """;

        var reader = new StringReader(source);

        var tokeniser = new HL7Tokeniser(reader);

        _segmentRepeatingTest = SegmentRepeatingTest.Read(tokeniser);
    }

    [Fact]
    public void IntegerArray()
    {
        Assert.Equal([2602, 1971], _segmentRepeatingTest.IntArray);
    }

    [Fact]
    public void FloatArray()
    {
        Assert.Equal([(float)3.14, (float)1.62, (float)2.72], _segmentRepeatingTest.FloatReadOnlyList);
    }

    [Fact]
    public void StringArray()
    {
        Assert.Equal<string[]>(["md", "sl"], _segmentRepeatingTest.StringArray);
    }

    [Fact]
    public void NullableStringArray()
    {
        Assert.Equal<string?[]>(["es", HL7Tokeniser.PresentButNull, "hm"], _segmentRepeatingTest.NullableStringArray);
    }

    [Fact]
    public void StringList()
    {
        Assert.Equal(["abc", "def"], _segmentRepeatingTest.StringList);
    }

    // TODO: Broken
    //[Fact]
    //public void NullableStringList()
    //{
    //    Assert.Equal(["ghi", "", "jkl", HL7Tokeniser.PresentButNull], _segmentRepeatingTest.NullableStringList);
    //}
}
