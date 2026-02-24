namespace ReflexHL7;

/// <summary>
/// Defines the HL7 MSH message.
/// </summary>
public class HL7_MSH
{
    /// <summary>
    /// Gets the MSH-2  field of message delimiters and other special characters.
    /// </summary>
    public required string EncodingCharacters { get; init; }

    /// <summary>
    /// Gets the MSH-3 data identifying the sending application.
    /// </summary>
    public required HL7_HD SendingApplication { get; init; }

    /// <summary>
    /// Gets the MSH-4 data identifying the sending facility.
    /// </summary>
    public HL7_HD? SendingFacility { get; init; }

    /// <summary>
    /// Gets the MSH-5 data identifying the receiving application.
    /// </summary>
    public HL7_HD? ReceivingApplication { get; init; }

    /// <summary>
    /// Gets the MSH-6 data identifying the receiving facility.
    /// </summary>
    public HL7_HD? ReceivingFacility { get; init; }

    /// <summary>
    /// Gets the MSH-7 date and time of message. If a time zone is specified, this is
    /// assumed to be the default time zone for any other time fields in the message.
    /// </summary>
    public required HL7_DTM? DateTimeOfMessage { get; init; }

    /// <summary>
    /// Gets the MSH-8 Security data.
    /// </summary>
    public string? Security { get; private init; }

    /// <summary>
    /// Gets the MSH-9 Message Type information.
    /// </summary>
    public required IReadOnlyList<string?> MessageType { get; init; }

    /// <summary>
    /// Gets the MSH-10 Message Control ID.
    /// </summary>
    public required string MessageControlId { get; init; }

    /// <summary>
    /// Gets the MSH-11 Processing ID.
    /// </summary>
    public required IReadOnlyList<string?> ProcessingId { get; init; }

    /// <summary>
    /// Gets the MSH-12 Version ID.
    /// </summary>
    public required IReadOnlyList<string?> VersionId { get; init; }

    /// <summary>
    /// Gets the MSH-13 Sequence Number.
    /// </summary>
    public string? SequenceNumber { get; private init; }

    /// <summary>
    /// Gets the MSH-14 Continuation Pointer.
    /// </summary>
    public string? ContinuationPointer { get; private init; }

    /// <summary>
    /// Gets the MSH-15 Accept Acknowledgment Type information.
    /// </summary>
    public string? AcceptAcknowledgmentType { get; private init; }

    /// <summary>
    /// Gets the MSH-16 Application Acknowledgment Type.
    /// </summary>
    public string? ApplicationAcknowledgmentType { get; private init; }

    /// <summary>
    /// Gets the MSH-17 Country Code (ID) 00017
    /// Definition: This field contains the country of origin for the message. It will be used primarily to specify default elements, such as currency denominations. The values to be used are those of ISO 3166,.6. The ISO
    /// 6 Available from ISO 1 Rue de Varembe, Case Postale 56, CH 1211, Geneve, Switzerland
    /// 3166 table has three separate forms of the country code: HL7 specifies that the 3-character (alphabetic) form be used for the country code.
    /// Refer to External Table 0399 - Country Code in Chapter 2C, Code Tables, for the 3-character codes as defined by ISO 3166-1.
    /// </summary>
    public string? CountryCode { get; private init; }

    /// <summary>
    /// Gets the MSH-18 Character Set.
    /// </summary>
    public string? CharacterSet { get; private init; }

    /// <summary>
    /// Gets the MSH-19 Principal Language of Message information.
    public IReadOnlyList<string?>? PrincipalLanguageOfMessage { get; private init; }

    /// <summary>
    /// Gets the MSH-20 Alternate Character Set Handling Scheme.
    /// </summary>
    public IReadOnlyList<string?>? AlternateCharacterSetHandlingScheme { get; private init; }

    /// <summary>
    /// Gets the MSH-21 Message Profile Identifier (EI) 01598
    /// Components: <Entity Identifier (ST)> ^ <Namespace ID (IS)> ^ <Universal ID (ST)> ^ <Universal ID Type (ID)>
    /// Chapter 2: Control
    /// Health Level Seven, Version 2.9© 2019. All rights reserved. Page 73
    /// Normative Publication. December 2019.
    /// Definition: Sites MAY use this field to assert adherence to, or reference, a message profile. Message profiles contain detailed explanations of grammar, syntax, and usage for a particular message or set of messages. See section 2B, "Conformance Using Message Profiles".
    /// Repetition of this field allows more flexibility in creating and naming message profiles. Using repetition, this field can identify a set of message profiles that the message conforms to. For example, the first repetition could reference a vendor's message profile. The second could reference another compatible provider's profile or a later version of the first vendor profile.
    /// As of v2.5, the HL7 message profile identifiers might be used for conformance claims and/or publish/subscribe systems. Refer to sections 2B.1.1 "Message profile identifier" and 2.B.1.2, "Message profile publish/subscribe topics" for details of the message profile identifiers. Refer to sections 2.B.4.1, "Static definition identifier" and 2.B.4.2, "Static definition publish/subscribe topics" for details of the static definition identifiers.
    /// Prior to v2.5, the field was called Conformance Statement ID. For backward compatibility, the Conformance Statement ID can be used here. Examples of the use of Conformance Statements appear in Chapter 5, "Query."
    /// </summary>
    public HL7_EI? MessageProfileIdentifier { get; private init; }

    /// <summary>
    /// Gets the MSH-22 Sending Responsible Organization (XON) 01823
    /// Definition: Business organization that originated and is accountable for the content of the message.
    /// Currently, MSH provides fields to transmit both sending/receiving applications and facilities (MSH.3 – MSH.6). However, these levels of organization do not necessarily relate to or imply a legal entity such as a business organization. As such, multiple legal entities (organizations) mightshare a service bureau, with the same application and facility identifiers. Another level of detail is required to delineate the various organizations using the same service bureau.
    /// Therefore, the Sending Responsible Organization field provides a complete picture from the application level to the overall business level. The Business Organization represents the legal entity responsible for the contents of the message.
    /// Use Case #1: A centralized system responsible for recording and monitoring instances of communicable diseases enforces a stringent authentication protocol with external applications that have been certified to access its information base. In order to allow message exchange, the centralized system mandates that external applications must provide the identity of the business organization sending the message (Sending Responsible Organization), the organization it is sending the message to (Receiving Responsible Organization, in this case the "owner" of the communicable diseases system), the network address from which the message has originated (Sending Network Address), the network address the message is being transmitted to (Receiving Network Address). The organization responsible for protecting the information
    /// stored within the communicable disease system requires this authentication due to the sensitive nature of the information it contains.
    /// </summary>
    public HL7_XON? SendingResponsibleOrganisation { get; private init; }

    /// <summary>
    /// Gets the MSH-23 Receiving Responsible Organization (XON) 01824
    /// Definition: Business organization that is the intended receiver of the message and is accountable for acting on the data conveyed by the transaction.
    /// This field has the same justification as the Sending Responsible Organization except in the role of the Receiving Responsible Organization. The receiving organization has the legal responsibility to act on the information in the message.
    /// See MSH-22 above for Use Case.
    /// </summary>
    public HL7_XON? ReceivingResponsibleOrganisation { get; private init; }

    /// <summary>
    /// Gets the MSH-24 Sending Network Address (HD) 01825
    /// Components: <Namespace ID (IS)> ^ <Universal ID (ST)> ^ <Universal ID Type (ID)>
    /// Definition: Identifier of the network location the message was transmitted from. Identified by an OID or text string (e.g., URI). The reader is referred to the "Report from the Joint W3C/IETF URI Planning Interest Group: Uniform Resource Identifiers (URIs), URLs, and Uniform Resource Names (URNs): Clarifications and Recommendations".7
    /// As with the Sending/Receiving Responsible Organization, the Sending Network Address provides a more detailed picture of the source of the message. This information is lower than the application layer, but is often useful/necessary for routing and identification purposes. This field SHOULD only be populated when the underlying communication protocol does not support identification of sending network locations.
    /// An agreement about the specific values and usage must exist among messaging partners. Use Case:
    /// Dr. Hippocrates works for the ''Good Health Clinic" (Sending facility) with a laptop running application XYZ (Sending App). He needs to talk to the provincial pharmacy system. He dials in and is assigned a network address. He then sends a message to the pharmacy system, which transmits a response back to him. Because the underlying network protocol does not have a place to communicate the sender and receiver network addresses, it therefore requires these addresses to be present in a known position in the payload.
    /// 7 The URI is: http://www.ietf.org/rfc/rfc3305.txt. Note: All IETF documents are available online, and RFCs are available through URIs using this format.
    /// There might be many doctors running application XYZ. In addition, the network address assigned to the laptop might change with each dial-in. This means there is not a 1..1 association between either the facility or the application and the network address.
    /// MSH||RX|GHC|||||OMP^O09^OMP_O09||||||||||||||||05782|
    /// Example 1: The Lone Tree Island satellite clinic transmits a notification of patient registration to its parent organization Community Health and Hospitals. The communication protocol does not support the identification of sending network location, so the sending network location is identified in the message by using its enterprise-wide network identifier "HNO2588".
    /// MSH||Reg|Lone|||||ADT^A04^ADT_A04||||||||||||||||HN02588|
    /// Example 2: The Stone Mountain satellite clinic transmits a notification of patient registration to its parent organization Community Health and Hospitals. The sending network location is identified by using its URI.
    /// MSH||Reg|Stone|||||ADT^A04^ADT_A04|||||||||||||||| ^ftp://www.goodhealth.org/somearea/someapp^URI|
    /// Example 3: The Three Rivers satellite clinic transmits a notification of patient registration to its parent organization Community Health and Hospitals. The sending network location is identified by using its Ipv4 address, port 5123 at node 25.152.27.69. The following example shows how to represent a port and DNS address using HD as the scheme
    /// MSH||Reg|TRC||||| ADT^A04^ADT_A04||||||||||||||||5123^25.152.27.69^DNS|
    /// Example 4: The Bayview satellite clinic transmits a notification of patient registration to its parent organization Community Health and Hospitals. The sending network location is identified by using "4086::132:2A57:3C28" its IPv6 address.
    /// MSH||REG|BAY||||| ADT^A04^ADT_A04||||||||||||||||^4086::132:2A57:3C28^IPv6|
    /// </summary>
    public HL7_HD? SendingNetworkAddress { get; private init; }

    /// <summary>
    /// Gets the MSH-25 Receiving Network Address (HD) 01826
    /// Components: <Namespace ID (IS)> ^ <Universal ID (ST)> ^ <Universal ID Type (ID)>
    /// Definition: Identifier of the network location the message was transmitted to. Identified by an OID or text string (e.g., URL).
    /// This is analogous with the Sending Network Address, however in the receiving role.
    /// This field SHOULD only be populated when the underlying communication protocol does not support identification receiving network locations
    /// </summary>
    public HL7_HD? ReceivingNetworkAddress { get; private init; }

    /// <summary>
    /// Gets the MSH-26 Security Classification Tag (CWE) 2429
    /// Definition: This field defines the security classification (as defined by ISO/IEC 2382-8:1998(E/F)/ T-REC-X.812-1995) of an IT resource, in this case the message, which MAY be used to make access control decisions.
    /// Conditionality Predicate: Required if MSH-27 or MSH-28 is valued, Optional if neither MSH-27 nor MSH-28 is valued."Use of this field supports the business requirement for declaring the level of confidentiality (classification) for a given message.
    /// Note: This field is used to declare the ‘high watermark’, meaning the most restrictive handling that needs to be applied to the message based on its content requiring a certain security classification level and SHOULD be viewed as the v2 equivalent of the document header in CDA
    /// </summary>
    public HL7_CWE? SecurityClassificationTag { get; private init; }

    internal HL7_MSH()
    {
    }

    internal static HL7_MSH Read(HL7Tokeniser tokeniser, string encodingCharacters)
    {
        tokeniser.ReadField();

        var sendingApplication = HL7_HD.Read(tokeniser);
        var sendingFacility = HL7_HD.Read(tokeniser);
        var receivingApplication = HL7_HD.Read(tokeniser);
        var receivingFacility = HL7_HD.Read(tokeniser);
        var date = tokeniser.ReadField();
        var dateTimeOfMessage = date is null ? null : HL7_DTM.Read(date);
        var security = tokeniser.ReadField();
        var messageType = tokeniser.ReadComponents().ToArray();
        var messageControlId = tokeniser.ReadField();
        var processingId = tokeniser.ReadComponents().ToArray();
        var versionId = tokeniser.ReadComponents().ToArray();
        var sequenceNumber = tokeniser.ReadField();
        var continuationPointer = tokeniser.ReadField();

        if (!string.IsNullOrEmpty(continuationPointer))
            throw HL7Tokeniser.CreateContinuationPointerException();

        var acceptAcknowledgmentType = tokeniser.ReadField();
        var applicationAcknowledgmentType = tokeniser.ReadField();
        var countryCode = tokeniser.ReadField();
        var characterSet = tokeniser.ReadField();
        var principalLanguageOfMessage = tokeniser.ReadComponents().ToArray();
        var alternateCharacterSetHandlingScheme = tokeniser.ReadComponents().ToArray();
        var messageProfileIdentifier = HL7_EI.Read(tokeniser);
        var receivingNetworkAddress = HL7_HD.Read(tokeniser);
        var receivingResponsibleOrganisation = HL7_XON.Read(tokeniser);
        var securityClassificationTag = HL7_CWE.Read(tokeniser);
        var sendingNetworkAddress = HL7_HD.Read(tokeniser);
        var sendingResponsibleOrganisation = HL7_XON.Read(tokeniser);

        tokeniser.SkipSegment();

        return new HL7_MSH
        {
            AcceptAcknowledgmentType = acceptAcknowledgmentType,
            ApplicationAcknowledgmentType = applicationAcknowledgmentType,
            CharacterSet = characterSet,
            ContinuationPointer = continuationPointer,
            CountryCode = countryCode,
            DateTimeOfMessage = dateTimeOfMessage,
            EncodingCharacters = encodingCharacters,
            MessageControlId = messageControlId!,
            MessageType = messageType,
            PrincipalLanguageOfMessage = principalLanguageOfMessage,
            ProcessingId = processingId,
            ReceivingApplication = receivingApplication,
            ReceivingFacility = receivingFacility,
            Security = security,
            SendingApplication = sendingApplication,
            SendingFacility = sendingFacility,
            SequenceNumber = sequenceNumber,
            VersionId = versionId,
            AlternateCharacterSetHandlingScheme = alternateCharacterSetHandlingScheme,
            MessageProfileIdentifier = messageProfileIdentifier,
            ReceivingNetworkAddress = receivingNetworkAddress,
            ReceivingResponsibleOrganisation = receivingResponsibleOrganisation,
            SecurityClassificationTag = securityClassificationTag,
            SendingNetworkAddress = sendingNetworkAddress,
            SendingResponsibleOrganisation = sendingResponsibleOrganisation,
        };
    }
}