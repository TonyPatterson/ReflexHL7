using System.Text;

namespace ReflexHL7;

/// <summary>
/// Defines the HL7 DTM data type. This defines a date, time and
/// time zone or a subset of those.
/// </summary>
public class HL7_DTM
{
    /// <summary>
    /// Gets the year, or null.
    /// </summary>
    public int? Year { get; private set; }

    /// <summary>
    /// Gets the month, or null.
    /// </summary>
    public int? Month { get; private set; }

    /// <summary>
    /// Gets the day, or null.
    /// </summary>
    public int? Day { get; private set; }

    /// <summary>
    /// Gets the hour, or null.
    /// </summary>
    public int? Hour { get; private set; }

    /// <summary>
    /// Gets the minute, or null.
    /// </summary>
    public int? Minute { get; private set; }

    /// <summary>
    /// Gets the second, or null.
    /// </summary>
    public int? Second { get; private set; }

    /// <summary>
    /// Gets the fraction of a second in ten-thousandths, or null.
    /// </summary>
    public int? TenThousandth { get; private set; }

    /// <summary>
    /// Gets the length of the fractional part of the time.
    /// This describes how many characters were used to
    /// express the fractional part of the seconds in the
    /// original data. For example, if the fraction is
    /// provided as ".542", the TenThousandth value will
    /// be 5420 and the FractionalLength will be 3.
    /// </summary>
    public int? FractionalLength { get; private set; }

    /// <summary>
    /// Gets the TimeZoneSign, or null if none was provided.
    /// </summary>
    public char? TimeZoneSign { get; private set; }

    /// <summary>
    /// Gets the time zone's hours offset, or null.
    /// </summary>
    public int? TimeZoneHourOffset { get; private set; }

    /// <summary>
    /// Gets the time zone's minutes offset, or null.
    /// </summary>
    public int? TimeZoneMinuteOffset { get; private set; }

    /// <summary>
    /// Returns true if a time zone specification was provided.
    /// </summary>
    public bool HasTimeZoneSpecification =>
        TimeZoneSign.HasValue && TimeZoneHourOffset.HasValue && TimeZoneMinuteOffset.HasValue;

    private StringBuilder? DateAndTimeToString()
    {
        if (!Year.HasValue)
            return null;

        var result = new StringBuilder($"{Year:D4}");

        if (!Month.HasValue)
            return result;

        result.Append($"{Month:D2}");

        if (!Day.HasValue)
            return result;

        result.Append($"{Day:D2}");

        if (!Hour.HasValue)
            return result;

        result.Append($"{Hour:D2}");

        if (!Minute.HasValue)
            return result;

        result.Append($"{Minute:D2}");

        if (!Second.HasValue)
            return result;

        result.Append($"{Second:D2}");

        if (!TenThousandth.HasValue)
            return result;

        string s = TenThousandth.Value.ToString();

        result.Append('.');
        result.Append(s.AsSpan(0, FractionalLength!.Value));

        return result;
    }

    /// <summary>
    /// Returns the date, time and time zone as a string.
    /// </summary>
    /// <returns>The string form of the object as it was read.</returns>
    public override string ToString()
    {
        var result = DateAndTimeToString();

        if (result is null)
            return string.Empty;

        if (HasTimeZoneSpecification)
            result.Append($"{TimeZoneSign!.Value}{TimeZoneHourOffset!.Value:D2}{TimeZoneMinuteOffset!.Value:D2}");

        return result.ToString();
    }

    /// <summary>
    /// Parse the provided string to create a HL7 DTM object.
    /// </summary>
    /// <param name="s">The text to parse.</param>
    /// <returns>An HL7_DTM object describing the time parsed
    /// from the given string.</returns>
    public static HL7_DTM? Read(string? s)
    {
        var target = new HL7_DTM();

        if (string.IsNullOrEmpty(s))
            return null;

        var parts = s!.Split('.', '-', '+');

        var sp = parts[0];
        int l = sp.Length;

        target.Year = int.Parse(sp.AsSpan(0, 4));

        if (l >= 6)
            target.Month = int.Parse(sp.AsSpan(4, 2));

        if (l >= 8)
            target.Day = int.Parse(sp.AsSpan(6, 2));

        if (l >= 10)
            target.Hour = int.Parse(sp.AsSpan(8, 2));

        if (l >= 12)
            target.Minute = int.Parse(sp.AsSpan(10, 2));

        if (l >= 14)
            target.Second = int.Parse(sp.AsSpan(12, 2));

        int separatorIndex = 0;

        for (int part = 1; part < parts.Length; part++)
        {
            separatorIndex += parts[part - 1].Length;

            char separator = s[separatorIndex++];
            string p = parts[part];

            if (separator == '.' && part == 1)
            {
                ExtractFractionalSecond(target, p);
            }
            else if ((separator == '+' || separator == '-') && part <= 2)
            {
                ExtractTimeZone(target, separator, p);
            }
            else
            {
                throw CreateInvalidFormatException();
            }
        }

        return target;
    }

    private static void ExtractTimeZone(HL7_DTM target, char c, string s)
    {
        switch (c)
        {
            case '+':
            case '-':
                break;

            default:
                throw CreateInvalidFormatException();
        }

        target.TimeZoneSign = c;

        target.TimeZoneHourOffset = int.Parse(s.AsSpan(0, 2));
        target.TimeZoneMinuteOffset = int.Parse(s.AsSpan(2, 2));
    }

    private static Exception CreateInvalidFormatException() =>
        new InvalidOperationException("Invalid time format");

    private static void ExtractFractionalSecond(HL7_DTM target, string s)
    {
        int l = s.Length;
        int i = 1;

        if (l > 4)
            throw new InvalidOperationException("Fractional time part too long");

        while (i < l && char.IsDigit(s[i]))
            i++;

        var fp = string.Concat(s, "000").AsSpan(0, 4);

        target.TenThousandth = int.Parse(fp);

        target.FractionalLength = s.Length;
    }

    /// <summary>
    /// Convert the object to a DateTimeOffset object.
    /// </summary>
    /// <returns>A DateTimeOffset with an equivalent value
    /// to the HL7_DTM.</returns>
    public DateTimeOffset? AsDateTimeOffset()
    {
        if (Year is null || Month is null || Day is null)
            return null;

        var offset = new TimeSpan(TimeZoneHourOffset ?? 0, TimeZoneMinuteOffset ?? 0, 0);

        if (TimeZoneSign == '-')
            offset = -offset;

        return new DateTimeOffset(
            Year.Value,
            Month.Value,
            Day.Value,
            Hour ?? 0,
            Minute ?? 0,
            Second ?? 0,
            (TenThousandth ?? 0) / 10,
            offset);
    }
}