namespace MessageTypes.Configuration;

public interface IProducerConfiguration
{
    string Broker { get; }
    string ProducerName { get; }
    string DefaultTopic { get; }
}