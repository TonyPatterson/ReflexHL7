using System.Globalization;
using Xunit;

namespace ReflexHL7.Tests;

public class HL7_DTMTests
{
    [Theory]
    [InlineData("1971", "1971")]
    [InlineData("1944+0030", "1944+0030")]
    [InlineData("2002-0000", "2002-0000")]
    [InlineData("197010", "197010")]
    [InlineData("197010+1000", "197010+1000")]
    [InlineData("197010-0230", "197010-0230")]
    [InlineData("20240226", "20240226")]
    [InlineData("20021017-1200", "20021017-1200")]
    [InlineData("20021017+1200", "20021017+1200")]
    [InlineData("20241017110846", "20241017110846")]
    [InlineData("19420830020304+0230", "19420830020304+0230")]
    [InlineData("19420830020304-0700", "19420830020304-0700")]
    [InlineData("19701113210944.4", "19701113210944.4")]
    [InlineData("19701113210944.45", "19701113210944.45")]
    [InlineData("19701113210944.456", "19701113210944.456")]
    [InlineData("19701113210944.4561", "19701113210944.4561")]
    [InlineData("19420830020304.9-0230", "19420830020304.9-0230")]
    [InlineData("19420830020304.88-0230", "19420830020304.88-0230")]
    [InlineData("19420830020304.777-0230", "19420830020304.777-0230")]
    [InlineData("19420830020304.6666-0230", "19420830020304.6666-0230")]
    [InlineData("19420830020304.5+0015", "19420830020304.5+0015")]
    [InlineData("19420830020304.44+0015", "19420830020304.44+0015")]
    [InlineData("19420830020304.333+0015", "19420830020304.333+0015")]
    [InlineData("19420830020304.1111+0015", "19420830020304.1111+0015")]
    public void RoundTripTest(string inValue, string expected)
    {
        HL7_DTM? dtm = HL7_DTM.Read(inValue);

        Assert.NotNull(dtm);

        string actual = dtm.ToString();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("19760704010159-0500", "07/04/1976 01:01:59 -05:00")]
    [InlineData("19760704010159-0400", "07/04/1976 01:01:59 -04:00")]
    [InlineData("198807050000", "07/05/1988 00:00:00 +00:00")]
    [InlineData("19880705", "07/05/1988 00:00:00 +00:00")]
    [InlineData("19981004010159+0100", "10/04/1998 01:01:59 +01:00")]
    public void TimeZoneTest(string inValue, string asDateTimeOffset)
    {
        HL7_DTM dtm = HL7_DTM.Read(inValue)!;

        var tzo = dtm.AsDateTimeOffset()!.Value;

        Assert.Equal(asDateTimeOffset, tzo.ToString(CultureInfo.InvariantCulture));
    }
}