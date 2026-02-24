using ReflexHL7;

namespace ReflexHL7.TestProject.HL7Examples.Schema;

public class Device
{
    public required HL7_OBX Obx { get; init; }

    public static Device Read(HL7Tokeniser tokeniser)
    {
        tokeniser.SkipSegments("DEV");

        var obx = HL7_OBX.Read(tokeniser);

        return new Device
        {
            Obx = obx
        };
    }
}