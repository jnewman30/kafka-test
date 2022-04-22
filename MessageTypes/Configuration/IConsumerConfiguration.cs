namespace MessageTypes.Configuration;

public interface IConsumerConfiguration
{
    string Broker { get; }
    string DefaultTopic { get; }
    string GroupId { get; }
    int BufferSize { get; }
    int WorkerCount { get; }
}