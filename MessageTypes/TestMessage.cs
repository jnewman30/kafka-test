using System.Runtime.Serialization;

namespace MessageTypes;

[DataContract]
public class TestMessage
{
    [DataMember(Order = 1)]
    public string? Text { get; set; }
}