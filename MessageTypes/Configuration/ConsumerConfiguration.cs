using Microsoft.Extensions.Configuration;

namespace MessageTypes.Configuration;

public class ConsumerConfiguration : IConsumerConfiguration
{
    public ConsumerConfiguration(IConfiguration configuration)
    {
        Configuration = configuration.GetSection("BrokerSettings");
    }
    private IConfiguration Configuration { get; }

    public string Broker => Configuration.GetValue<string>("Broker");
    
    public string DefaultTopic => Configuration.GetValue<string>("DefaultTopic");
    
    public string GroupId => Configuration.GetValue<string>("GroupId");
    
    public int BufferSize => Configuration.GetValue<int>("BufferSize");
    
    public int WorkerCount => Configuration.GetValue<int>("WorkerCount");
}