# ReflexHL7

This is a .Net Source Code Generator developed to read
[HL7 V2](http://www.hl7.org/implement/standards/product_section.cfm?section=13)
(Health Level 7 Version 2 messaging standard).

The generator is a lightweight way of building HL7 V2 parsing in to a .Net
application by helping clients define classes which map to HL7 messages and
their components. This allows clients to create streamlined parsers which
only read messages and fields of interest, giving the resulting code low
memory demands and high performance.

It also means that the POCO classes generated are easily used by the client
system without having to first be translated in to more manageable classes.

## Build Status

[![Test and Publish](https://github.com/TonyPatterson/ReflexHL7/actions/workflows/TestAndPublish.yml/badge.svg)](https://github.com/TonyPatterson/ReflexHL7/actions/workflows/TestAndPublish.yml)

## Usage

An example of the code required to read the account number of a patient
would look like this:

```
using System.IO;
using ReflexHL7;

namespace MyHL7App;

internal class Program
{
    private static void Main(string[] args)
    {
        using var s = File.OpenText(args[0]);

        var msg = HL7_VanillaMessage.Read(s);

        Console.WriteLine($"Patient account number: {msg.PID.PatientAccountNumber}");
    }
}

[HL7MessageDefinition]
public partial class HL7_VanillaMessage
{
    [HL7Segment("PID")]
    public required HL7_PID_Minimal PID { get; init; }
}

[HL7SegmentDefinition("PID")]
public partial class HL7_PID_Minimal
{
    [HL7Field(18)]
    public string? PatientAccountNumber { get; init; }
}
```

In this program, we first open a stream to an input file containing our
message. We then call the static Read method on the message type that
we have defined using the two classes below. We then have straightforward
access to the properties loaded from the file, through those classes.

*Note that this example assumes that the message contains a PID segment
immediately after the MSH segment. If that is not the case, the number
will not be read. A more complete message definition would be needed.*

In this simple example we have only mapped out a single property of
interest, but it is possible to map out every property at every level of
whichever message that you would like to read. In many use cases it
will be beneficial only to map out the fields that you plan to use. It
will always be possible to add fields in later on.

## Mapping Classes

To create a mapping class, it is necessary to do three things:

1. Mark the class as `public` and `partial`. It is necessary to do this
   so the code generator can add the additional methods
   `<MappingClass> Read(HL7Tokeniser tokeniser)` and
   `<MappingClass> Read(System.IO.TextReader s)` in the generated code.
2. Add the HL7 definition attribute to your class. This attribute tells
   the code generator which classes you wish to create `Read` methods for.
3. Add properties with property mapping attributes.

The following table shows what attributes should be used for mapping the different HL7 entities.

| HL7 Entity | Definition Attribute   | Field Mapping Attribute |
| ---------- | ---------------------- | ----------------------- |
| Message    | HL7MessageDefinition   | HL7Segment              |
| Segment    | HL7SegmentDefinition   | HL7Field                |
| Field      | HL7FieldDefinition     | HL7Component            |
| Component  | HL7ComponentDefinition | HL7SubComponent         |

## Property Mappings

There are various different target types that your property mappings can
be defined for:

### string

The simplest target is a string which will be loaded with the complete
content of the source entity. It is recommended that you use nullable
strings as the HL7 content is not usually guaranteed to be set. When
using a string, the field content is used directly, without processing
escapes or formatting characters.

### HL7String

When an `HL7String` is specified, the field content is interpreted as a
string, but simple escape sequences are converted to their intended
characters and other escapes are stored within the object. The object
created is a collection of `HL7StringComponent` objects which define
the type of escape (or content) and the content. It is up to the client
application to handle character set escapes, formatting characters,
highlighting and truncation.

### IReadOnlyList<T>

When an entity has distinct sub-entities you can use an `IReadOnlyList<T>`
to get them all as a collection. The list is guaranteed to be non-null, but
it may be empty. It will not contain null strings.

### HL7_DTM

The HL7_DTM class is provided to parse HL7 date/time (DTM) entities. This
can be used in mappings requiring a date, time and time zone.

### byte[]

The byte array type can be used to read binary data from HL7 messages. The data is
assumed to be base64 encoded.

### Other Types

It is possible to use any other type as a mapping as long as it defines
a static method `<PropertyClass> Read(HL7Tokeniser tokeniser)`. The
tokeniser will use this method to load the property. Because the generated
`Read` methods match this signature, they can be directly used for
mapped properties. For this to work properly, the correct hierarchy
must be observed. For example when defining a property on a segment,
the mappings should use the `HL7Field` attribute and classes used to
map properties should either be marked with `HL7FieldDefinition` or should
correctly read a complete field in its `<PropertyClass> Read(HL7Tokeniser tokeniser)`
method.

# Support for the HL7 Standard
This is not a complete HL7 implementation and the following are either unsupported
or partially supported:

* **Continuation messages** These are not currently supported.
* **Message writing** Message writing is not supported. If it is supported in future, the support may not include full round-trip capabilities.
* **Formatting codes** These can be read and parsed, but are not interpeted in any way. Due to the various types of escapes in HL7 text content, there is no obvious canonical solution.
* **Full schema definition** It is not the intent of the library to provide full implementations of all HL7 messages, although this could be supported by an ancillary library in the future.

# Development

## Code Generation Updates

Open a terminal and run the following command to get rapid feedback
on updates to the code generators as you code. The generated outputs
can be opened from the **Generated Code** solution folder and you will
see code generation updates reflected near-instantaneously.

```
dotnet watch build -verbosity:diag
```

When compiling the project within Visual Studio, the code generator will
be cached, so updates to its behaviour won't usually be seen until Visual
Studio has been restarted.

## Debugging Code Generators

Run the **ReflexHL7.CodeGenerator** project's **Build Test Project** profile to debug code generation.

## Benchmarking

Run the **ReflexHL7.Benchmark** release build, outside the debugger to measure
performance.
