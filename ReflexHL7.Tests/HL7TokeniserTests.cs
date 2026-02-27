using ReflexHL7.TestProject.HL7Examples.Schema;

namespace ReflexHL7.Tests;

public class HL7TokeniserTests
{
    [Fact]
    public void HL7TokeniserTest()
    {
        using var str = File.OpenText(@"Data\ORU_R01.hl7");

        HL7Tokeniser tokeniser = new(str);

        var msh = tokeniser.MshRecord;

        Assert.Multiple(
            () => Assert.Equal("AL", msh.AcceptAcknowledgmentType),
            () => Assert.Equal("NE", msh.ApplicationAcknowledgmentType),
            () => Assert.Equal("UTF", msh.CharacterSet),
            () => Assert.Equal(string.Empty, msh.ContinuationPointer),
            () => Assert.Equal("UK", msh.CountryCode),
            () => Assert.Equal("20120411070545", msh.DateTimeOfMessage!.ToString()),
            () => Assert.Equal("^~\\&", msh.EncodingCharacters),
            () => Assert.Equal("59689", msh.MessageControlId),
            () => Assert.Equal(["EN"], msh.PrincipalLanguageOfMessage),
            () => Assert.Equal(["T"], msh.ProcessingId),
            () => Assert.Equal("SD", msh.Security),
            () => Assert.Equal("seqn", msh.SequenceNumber),
            () => Assert.Equal(["2.5.1"], msh.VersionId),
            () => Assert.Equal((string[])["ORU", "R01"], msh.MessageType),
            () => Assert.Equal("RA3", msh.ReceivingApplication!.NamespaceId),
            () => Assert.Equal("RF4", msh.ReceivingFacility!.NamespaceId),
            () => Assert.Equal("SA1", msh.SendingApplication!.NamespaceId),
            () => Assert.Equal("SF2", msh.SendingFacility!.NamespaceId));
    }

    [Fact]
    public void GeneratedReader_IncludesMsh()
    {
        using var str = File.OpenText(@"Data\ORU_R01.hl7");

        HL7Tokeniser tokeniser = new(str);

        HL7_ORU_R01_Partial oru = HL7_ORU_R01_Partial.Read(tokeniser);

        Assert.Same(tokeniser.MshRecord, oru.MSH);
    }

    [Fact]
    public void GeneratedReader_ReadStringField()
    {
        HL7_ORU_R01_Partial oru = ReadOruSample();

        Assert.Equal("F", oru.PatientResult.Single().Patient.Pid.AdministrativeSex);
    }

    [Fact]
    public void GeneratedReader_ReadStringField_PresentButNull()
    {
        HL7_ORU_R01_Partial oru = ReadOruSample();

        Assert.Equal(HL7Tokeniser.PresentButNull, oru.PatientResult.Single().Patient.Pid.PrimaryLanguage);
    }

    [Fact]
    public void GeneratedReader_ReadStringArrayField()
    {
        HL7_ORU_R01_Partial oru = ReadOruSample();

        string?[] expected = [
            "N",
            "Not Hispanic or Latino",
            "HL0189",
            HL7Tokeniser.PresentButNull,
            "END"
            ];

        Assert.Equal(expected, oru.PatientResult.Single().Patient.Pid.EthnicGroup);
    }

    [Fact]
    public void GeneratedReader_ReadDateTimeField()
    {
        HL7_ORU_R01_Partial oru = ReadOruSample();

        Assert.NotNull(oru.PatientResult.Single().Patient.Pid.DateTimeOfBirth);

        Assert.Equal("19820304", oru.PatientResult.Single().Patient.Pid.DateTimeOfBirth!.ToString());
    }

    [Fact]
    public void GeneratedReader_ReadCustomField()
    {
        HL7_ORU_R01_Partial oru = ReadOruSample();

        HL7_XPN? patientName = oru.PatientResult.Single().Patient.Pid.PatientName;

        Assert.NotNull(patientName);

        Assert.Equal("Clément", patientName.FamilyName);
        Assert.Equal("Suzanne", patientName.GivenName);
        Assert.Equal("Charlotte", patientName.FurtherGivenNamesOrInitials);
        Assert.Equal("s", patientName.Suffix);
        Assert.Equal("p", patientName.Prefix);
        Assert.Equal("MA", patientName.Degree);
        Assert.Equal("L", patientName.NameTypeCode);
        Assert.Null(patientName.NameRepresentationCode);
        Assert.Null(patientName.NameContext);
        Assert.Null(patientName.NameValidityRange);
        Assert.Null(patientName.NameAssemblyOrder);
        Assert.Null(patientName.EffectiveDate);
        Assert.Null(patientName.ExpirationDate);
        Assert.Null(patientName.ProfessionalSuffix);
        Assert.Null(patientName.CalledBy);
    }

    private static HL7_ORU_R01_Partial ReadOruSample()
    {
        var str = File.OpenText(@"Data\ORU_R01.hl7");

        HL7Tokeniser tokeniser = new(str);

        return HL7_ORU_R01_Partial.Read(tokeniser);
    }

    [Fact]
    public void Tokeniser_UnsupportedVersionThrowsException()
    {
        var str = new StringReader(
            """
            MSH|^~\&|SA|SF|NHS|PAS|||MDM^T02|1143|P|2.4.1|||NE|AL|GBR|UTF-8|EN|
            EVN||20210801181017|
            """);

        var reader = new HL7Tokeniser(str);

        var exc = Assert.Throws<HL7UnsupportedVersionException>(() => HL7_MDM_T02_Partial.Read(reader));

        Assert.Equal("Message reader does not support version 2.4.1", exc.Message);
    }

    [Fact]
    public void Tokeniser_NotImplementedThrownWhenContinuationFound()
    {
        var str = new StringReader(
            """
            MSH|^~\&|SA|SF|NHS|PAS|||MDM^T02|1143|P|2.4||Continuation|NE|AL|GBR|UTF-8|EN|
            EVN||20210801181017|
            """);

        var exc = Assert.Throws<NotImplementedException>(() => new HL7Tokeniser(str));

        Assert.Equal("Continuation messages are not supported.", exc.Message);
    }

    [Fact]
    public void Tokeniser_NotImplementedThrownWhenDscSegmentFound()
    {
        var str = new StringReader(
            """
            MSH|^~\&|SA|SF|NHS|PAS|||MDM^T02|1143|P|2.4|||NE|AL|GBR|UTF-8|EN|
            EVN||20210801181017|
            DSC|Continuation
            """);

        var reader = new HL7Tokeniser(str);

        var exc = Assert.Throws<NotImplementedException>(() => HL7_MDM_T02_Partial.Read(reader));

        Assert.Equal("Continuation messages are not supported.", exc.Message);
    }

    [Fact]
    public void GeneratedReader_CorrectObservationCount()
    {
        HL7_ORU_R01_Partial oru = ReadOruSample();

        var observations = oru.PatientResult.Single().OrderObservation.Single().Observation;

        Assert.Equal(14, observations.Count);
    }

    [Fact]
    public void GeneratedReader_CorrectObservationContent()
    {
        HL7_ORU_R01_Partial oru = ReadOruSample();

        string?[]? obsId = oru.PatientResult.Single().OrderObservation.Single().Observation[5].Obx?.ObservationIdentifier;

        Assert.NotNull(obsId);
        Assert.Equal<string?[]?>(
            ["baso", "Baso", "Local", "706-2", "Baso", "LN"],
            obsId!);
    }
}