using System.Collections;
using System.Text;

namespace ReflexHL7;

public class HL7String : IReadOnlyList<HL7StringComponent>
{
    private readonly List<HL7StringComponent> _components = [];

    public int Count => _components.Count;

    public HL7StringComponent this[int index] => _components[index];

    public IEnumerator<HL7StringComponent> GetEnumerator() => _components.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _components.GetEnumerator();

    public HL7String(HL7Tokeniser tokeniser, string source)
    {
        StringBuilder sb = new();
        StringBuilder sbEscape = new();
        int length = source.Length;
        int i = 0;

        while (i < length)
        {
            char c = source[i++];

            if (c == tokeniser._escapeCharacter)
            {
                sbEscape.Clear();

                while (i < length)
                {
                    char ec = source[i++];

                    if (ec == tokeniser._escapeCharacter)
                        break;

                    sbEscape.Append(ec);
                }

                c = MapToSimpleCharacter(tokeniser, sbEscape);

                if (c == 0)
                {
                    OutputNonEscapeString();

                    sb.Clear();

                    _components.Add(CreateEscape(sbEscape));

                    continue;
                }
            }

            sb.Append(c);
        }

        OutputNonEscapeString();

        void OutputNonEscapeString()
        {
            if (sb.Length > 0)
                _components.Add(new HL7StringComponent(HL7StringComponentType.Text, sb.ToString()));
        }
    }

    private static HL7StringComponent CreateEscape(StringBuilder sbEscape)
    {
        string s = sbEscape.ToString();

        if (s.Length > 1)
        {
            switch (s[0])
            {
                case 'C': return MapParameterised(HL7StringComponentType.SingleByteCharacterSet);
                case 'M': return MapParameterised(HL7StringComponentType.MultipleByteCharacterSet);
                case 'X': return MapParameterised(HL7StringComponentType.Hexadecimal);
                case 'Z': return MapParameterised(HL7StringComponentType.LocallyDefined);
            }
        }

        return s switch
        {
            "H" => new HL7StringComponent(HL7StringComponentType.Highlight),
            "N" => new HL7StringComponent(HL7StringComponentType.Normal),
            "P" => new HL7StringComponent(HL7StringComponentType.Truncation),
            _ => MapFormatting(s)
        };

        HL7StringComponent MapParameterised(HL7StringComponentType type) => new(type, s[1..]);
    }

    private static HL7StringComponent MapFormatting(string s)
    {
        if (s[0] == '.' && s.Length >= 3)
        {
            bool recognised = s[1..3] switch
            {
                "br" or "sp" or "nf" or "fi" or
                "in" or "ti" or "sk" or "ce" => true,
                _ => false
            };

            if (recognised)
                return new HL7StringComponent(HL7StringComponentType.Formatting, s);
        }

        throw new InvalidOperationException("Unrecognised escape sequence.");
    }

    private static char MapToSimpleCharacter(HL7Tokeniser tokeniser, StringBuilder sbEscape)
    {
        if (sbEscape.Length != 1)
            return (char)0;

        return sbEscape[0] switch
        {
            'F' => tokeniser._fieldSeparator,
            'S' => tokeniser._componentSeparator,
            'T' => tokeniser._subComponentSeparator,
            'R' => tokeniser._repetitionSeparator,
            'E' => tokeniser._escapeCharacter,
            _ => (char)0
        };
    }

    public override string ToString()
    {
        return string.Concat(
            from c in _components
            where c.Type == HL7StringComponentType.Text
            select c.Content);
    }
}