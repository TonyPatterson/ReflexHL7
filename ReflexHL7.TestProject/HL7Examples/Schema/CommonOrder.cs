using ReflexHL7;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

public class CommonOrder
{
    public static CommonOrder Read(HL7Tokeniser tokeniser)
    {
        tokeniser.SkipSegments("ORC");
        tokeniser.SkipSegments("PRT");
        var orderDocument = OrderDocument.Read(tokeniser);

        return new CommonOrder
        {
        };
    }
}