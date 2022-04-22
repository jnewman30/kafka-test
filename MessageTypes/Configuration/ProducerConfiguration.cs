using Microsoft.Extensions.Configuration;

namespace MessageTypes.Configuration;

public class ProducerConfiguration : IProducerConfiguration
{
    public ProducerConfiguration(IConfiguration configuration)
    {
        Configuration = configuration.GetSection("BrokerSettings");
    }

    private IConfiguration Configuration { get; }

    public string Broker => Configuration.GetValue<string>("Broker");

    public string ProducerName => Configuration.GetValue<string>("ProducerName");
    
    public string DefaultTopic => Configuration.GetValue<string>("DefaultTopic");
}