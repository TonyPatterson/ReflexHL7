using System.Text;

namespace ReflexHL7;

public class HL7Tokeniser
{
    // No benefit observed using on any methods with [MethodImpl(MethodImplOptions.AggressiveInlining)]

    // Ordering is important here, values must be +ve in
    // ascending order of superiority
    internal const int RepetitionSeparator = 0x70000000;
    internal const int SubComponentSeparator = 0x70000001;
    internal const int ComponentSeparator = 0x70000002;
    internal const int FieldSeparator = 0x70000003;
    internal const int SegmentSeparator = 0x70000004;
    internal const int EndOfFile = 0x70000005;
    internal const int SegmentName = 0x70000006;

    internal readonly char _fieldSeparator;
    internal readonly char _componentSeparator;
    internal readonly char _repetitionSeparator;
    internal readonly char _escapeCharacter;
    internal readonly char _subComponentSeparator;
    private string? _segmentName;

    private readonly TextReader _inputStream;

    private readonly HL7_MSH _msh;

    private bool _previousWasLineSeparator = false;
    private bool _honourRepetitions = false;
    private int _streamPosition = 0;
    private int _pushedToken = 0;

    public int StreamPosition => _streamPosition;

    /// <summary>
    /// Gets the MSH record from the file being read.
    /// </summary>
    public HL7_MSH MshRecord => _msh;

    public string CurrentSegmentName
    {
        get
        {
            if (_segmentName is null)
            {
                _segmentName = ReadSegmentName();

                // An empty string means we have no more segments to read
                if (_segmentName.Length == 0)
                    return _segmentName;

                int fs = _inputStream.Read();

                if (fs != _fieldSeparator)
                    throw new InvalidOperationException("HL7 segment name terminator missing");
            }

            return _segmentName;
        }
    }

    private string ReadSegmentName()
    {
        Span<char> segmentName = stackalloc char[3];

        for (int i = 0; i < segmentName.Length;)
        {
            int c = _inputStream.Read();

            if (c == '\r' || c == '\n')
                continue;

            if (!IsValidSegmentNameCharacter(c))
            {
                if (c < 0)
                    return string.Empty;

                throw new InvalidOperationException("Could not read HL7 segment name");
            }

            segmentName[i++] = (char)c;
        }

        string name = new(segmentName);

        if (name == "DSC")
            throw CreateContinuationPointerException();

        return name;

        static bool IsValidSegmentNameCharacter(int c)
        {
            if (c >= 'A' && c <= 'Z')
                return true;

            return c >= '0' && c <= '9';
        }
    }

    internal static NotImplementedException CreateContinuationPointerException() =>
        new("Continuation messages are not supported.");

    /// <summary>
    /// Create an instance of the HL7 file reader based on the
    /// provied TextReader.
    /// </summary>
    /// <param name="inputStream">The stream from which to read the
    /// HL7 message data from.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public HL7Tokeniser(TextReader inputStream)
    {
        _inputStream = inputStream;

        Span<char> buffer = stackalloc char[8];

        int count = _inputStream.ReadBlock(buffer);

        if (count != buffer.Length ||
            buffer[0] != 'M' ||
            buffer[1] != 'S' ||
            buffer[2] != 'H')
            throw new InvalidOperationException("Not an HL7 stream");

        _fieldSeparator = buffer[3];
        _componentSeparator = buffer[4];
        _repetitionSeparator = buffer[5];
        _escapeCharacter = buffer[6];
        _subComponentSeparator = buffer[7];
        _segmentName = "MSH";

        _msh = HL7_MSH.Read(this, buffer.Slice(4, 4).ToString());
    }

    public void VerifyVersion(params string[] acceptedVersions)
    {
        if (acceptedVersions is null)
            return;

        string? thisVersion = _msh.VersionId?[0];

        if (thisVersion is null || !acceptedVersions.Contains(thisVersion))
            throw new HL7UnsupportedVersionException($"Message reader does not support version {thisVersion}");
    }

    private bool ReadPart(int separator, StringBuilder? sb)
    {
        bool hasContent = false;

        while (true)
        {
            int c = ReadToken();

            if (c >= separator || (_honourRepetitions && c == RepetitionSeparator))
            {
                if (c == separator)
                    return true;

                PushToken(c);

                return hasContent;
            }

            sb?.Append(TokenToCharacter(c));

            hasContent = true;
        }
    }

    private char TokenToCharacter(int t)
    {
        // For fields which aren't unpacked, revert the token values
        // to the characters they replaced.
        int c = t switch
        {
            RepetitionSeparator => _repetitionSeparator,
            SubComponentSeparator => _subComponentSeparator,
            ComponentSeparator => _componentSeparator,
            _ => t
        };

        return (char)c;
    }

    private Element ReadPart(int separator)
    {
        int initialPosition = _streamPosition;
        var sb = new StringBuilder();

        if (!ReadPart(separator, sb))
            return (null, true);

        string? s = sb.ToString();

        if (_streamPosition == initialPosition + 3)
        {
            if (s == "\"\"")
                s = null;
        }

        return (s, false);
    }

    private void SkipPart(int separator) =>
        ReadPart(separator, null);

    /// <summary>
    /// Read a field from the HL7 stream and return the content.
    /// </summary>
    /// <returns>The string content of the field.</returns>
    public string? ReadField() =>
        ReadPart(FieldSeparator).Content;

    /// <summary>
    /// Read a component from the HL7 stream and return the content.
    /// </summary>
    /// <returns>The string content of the component.</returns>
    public string? ReadComponent() =>
        ReadPart(ComponentSeparator).Content;

    /// <summary>
    /// Read a sub-component from the HL7 stream and return the content.
    /// </summary>
    /// <returns>The string content of the sub-component.</returns>
    public string? ReadSubComponent() =>
        ReadPart(SubComponentSeparator).Content;

    /// <summary>
    /// Read all of the components from the current field in the HL7 stream
    /// and return each with an a enumerator.
    /// </summary>
    /// <returns>An enumeration of the string content of the components.</returns>
    public IEnumerable<string?> ReadComponents()
    {
        while (true)
        {
            var c = ReadPart(ComponentSeparator);

            if (c.Exhausted)
                break;

            string? s = c.Content;

            yield return s is null || s.Length == 0 ? null : s;
        }

        SkipField();
    }

    /// <summary>
    /// Read all of the sub-components from the current component in the HL7 stream
    /// and return each with an a enumerator.
    /// </summary>
    /// <returns>An enumeration of the string content of the sub-components.</returns>
    public IEnumerable<string?> ReadSubComponents()
    {
        while (true)
        {
            var c = ReadPart(SubComponentSeparator);

            if (c.Exhausted)
                break;

            yield return c.Content;
        }

        SkipComponent();
    }

    /// <summary>
    /// Move the stream on to the beginning of the next segment.
    /// </summary>
    public void SkipSegment()
    {
        SkipPart(SegmentSeparator);

        _segmentName = null;
    }

    /// <summary>
    /// Move the stream on to the beginning of the next field.
    /// </summary>
    public void SkipField() =>
        SkipPart(FieldSeparator);

    /// <summary>
    /// Move the stream on to the beginning of the next component.
    /// </summary>
    public void SkipComponent() =>
        SkipPart(ComponentSeparator);

    /// <summary>
    /// Move the stream on to the beginning of the next sub-component.
    /// </summary>
    public void SkipSubComponent() =>
        SkipPart(SubComponentSeparator);

    private int ReadToken()
    {
        if (_pushedToken > 0)
        {
            int t = _pushedToken;

            _pushedToken = 0;

            return t;
        }

        while (true)
        {
            int t = ReadTokenBase();

            if (t != SegmentSeparator)
            {
                _previousWasLineSeparator = false;

                return t;
            }

            if (!_previousWasLineSeparator)
            {
                _previousWasLineSeparator = true;

                return t;
            }
        }

        int ReadTokenBase()
        {
            int c = _inputStream.Read();

            if (c < 0)
                return EndOfFile;

            _streamPosition++;

            if (c == _componentSeparator)
                return ComponentSeparator;

            if (c == _repetitionSeparator)
                return RepetitionSeparator;

            if (c == _fieldSeparator)
                return FieldSeparator;

            if (c == _subComponentSeparator)
                return SubComponentSeparator;

            if (c == '\r' || c == '\n')
                return SegmentSeparator;

            return c;
        }
    }

    private void PushToken(int token) => _pushedToken = token;

    public IEnumerable<T> ReadRepetitions<T>(Func<HL7Tokeniser, T> readFunction)
    {
        _honourRepetitions = true;

        while (true)
        {
            yield return readFunction(this);

            int t = ReadToken();

            if (t != RepetitionSeparator)
            {
                PushToken(t);

                break;
            }
        }

        _honourRepetitions = false;
    }

    public void SkipSegments(string segmentToSkip)
    {
        while (CurrentSegmentName == segmentToSkip)
            SkipSegment();
    }

    private record struct Element(string? Content, bool Exhausted)
    {
        public static implicit operator (string? Content, bool Exhausted)(Element value)
        {
            return (value.Content, value.Exhausted);
        }

        public static implicit operator Element((string? Content, bool Exhausted) value)
        {
            return new Element(value.Content, value.Exhausted);
        }
    }
}