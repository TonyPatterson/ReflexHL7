namespace ReflexHL7.Tests;

[HL7SegmentDefinition("XXX")]
public partial class SegmentTest
{
    [HL7Field(1)]
    public int i1 { get; set; }

    [HL7Field(2)]
    public int? i2 { get; set; }

    [HL7Field(3)]
    public decimal d1 { get; set; }

    [HL7Field(4)]
    public decimal? d2 { get; set; }

    [HL7Field(5)]
    public double dbl1 { get; set; }

    [HL7Field(6)]
    public double? dbl2 { get; set; }

    [HL7Field(7)]
    public float flt1 { get; set; }

    [HL7Field(8)]
    public float? flt2 { get; set; }

    [HL7Field(9)]
    public string s1 { get; set; }

    [HL7Field(10)]
    public string? s2 { get; set; }

    [HL7Field(11)]
    public string? s3 { get; set; }

    [HL7Field(12)]
    public string? s4 { get; set; }

    [HL7Field(13)]
    public int? iNull { get; set; }

    [HL7Field(13)]
    public decimal? dNull { get; set; }

    [HL7Field(13)]
    public float? fltNull { get; set; }

    [HL7Field(13)]
    public double? dblNull { get; set; }
}

public class FieldDeserialisationTests
{
    private SegmentTest _segmentTest;

    public FieldDeserialisationTests()
    {
        const string source = """
            MSH|^~\&|||||||MDM^T02|A||2.4|
            XXX|2602|1971|19.99|7.17|44.72|44.68|78.19|92.220|es|hm||""|||||
            """;

        var reader = new StringReader(source);

        var tokeniser = new HL7Tokeniser(reader);

        _segmentTest = SegmentTest.Read(tokeniser);
    }

    [Fact]
    public void Integer()
    {
        Assert.Equal(2602, _segmentTest.i1);
        Assert.Equal(1971, _segmentTest.i2);
    }

    [Fact]
    public void Decimal()
    {
        Assert.Equal(19.99m, _segmentTest.d1);
        Assert.Equal(7.17m, _segmentTest.d2);
    }

    [Fact]
    public void Float()
    {
        Assert.Equal((float)78.19, _segmentTest.flt1);
        Assert.Equal((float)92.220, _segmentTest.flt2);
    }

    [Fact]
    public void Double()
    {
        Assert.Equal(44.72, _segmentTest.dbl1);
        Assert.Equal(44.68, _segmentTest.dbl2);
    }

    [Fact]
    public void String()
    {
        Assert.Equal("es", _segmentTest.s1);
        Assert.Equal("hm", _segmentTest.s2);

        // Empty field is an empty string
        Assert.Equal("", _segmentTest.s3);

        // Field with "" is null
        Assert.Equal(HL7Tokeniser.PresentButNull, _segmentTest.s4);
    }

    [Fact]
    public void NumericNulls()
    {
        Assert.Null(_segmentTest.iNull);
        Assert.Null(_segmentTest.fltNull);
        Assert.Null(_segmentTest.dblNull);
        Assert.Null(_segmentTest.dNull);
    }
}
