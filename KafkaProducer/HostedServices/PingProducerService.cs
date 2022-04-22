using KafkaFlow;
using MessageTypes;
using MessageTypes.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaProducer.HostedServices;

public class PingProducerService : BackgroundService
{
    public PingProducerService(
        ILogger<PingProducerService> logger,
        IProducerConfiguration producerConfiguration,
        IKafkaBus bus)
    {
        Logger = logger;
        ProducerConfiguration = producerConfiguration;
        Bus = bus;
    }

    private ILogger<PingProducerService> Logger { get; }
    private IProducerConfiguration ProducerConfiguration { get; }
    private IKafkaBus Bus { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("{Service} Started at {Time}",
            nameof(PingProducerService), DateTimeOffset.Now);

        await Bus.StartAsync(stoppingToken);

        var producer = Bus.Producers.GetProducer(ProducerConfiguration.ProducerName);

        while (!stoppingToken.IsCancellationRequested)
        {
            Produce(producer);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private void Produce(IMessageProducer producer)
    {
        var messageKey = Guid.NewGuid();
        var message = new TestMessage { Text = $"Test Message - {DateTimeOffset.Now} - {messageKey}" };
        
        Logger.LogInformation("Producing Message : {Message}",
            message.Text);

        producer.Produce(messageKey, message);


    }
}