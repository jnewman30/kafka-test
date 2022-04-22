using KafkaFlow;
using KafkaFlow.TypedHandler;
using MessageTypes;
using Microsoft.Extensions.Logging;

namespace KafkaConsumer.Handlers;

public class TestMessageHandler : IMessageHandler<TestMessage>
{
    public Task Handle(IMessageContext context, TestMessage message)
    {
        // Logger.LogInformation(
        //     "Partition: {Partition} | Offset: {Offset} | Message: {Message}",
        //     context.ConsumerContext.Partition,
        //     context.ConsumerContext.Offset,
        //     message.Text);

        Console.WriteLine(
            "Partition: {0} | Offset: {1} | Message: {2}",
            context.ConsumerContext.Partition,
            context.ConsumerContext.Offset,
            message.Text);

        return Task.CompletedTask;        
    }
}