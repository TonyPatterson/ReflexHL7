using System.Text;

namespace ReflexHL7.CodeGenerator;

internal class CodeStringBuilder
{
    private string _indentString = string.Empty;
    private readonly StringBuilder _sb = new();
    private int _indent = 0;

    public void AppendLine(string s) => _sb.AppendLine(_indentString + s);

    public void AppendLine() => _sb.AppendLine(string.Empty);

    public void Indent(int increase = 1) => SetIndent(_indent + increase);

    public void Unindent() => SetIndent(_indent - 1);

    private void SetIndent(int newIndent)
    {
        _indent = newIndent;

        if (_indent < 0)
            throw new InvalidOperationException("-ve indent");

        _indentString = new string(' ', newIndent * 4);
    }

    public void Append(CodeStringBuilder csb) => _sb.Append(csb.ToString());

    public override string ToString() => _sb.ToString();
}