// See https://aka.ms/new-console-template for more information

using KafkaConsumer.Handlers;
using KafkaFlow;
using KafkaFlow.Serializer;
using KafkaFlow.TypedHandler;
using MessageTypes.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder()
    .ConfigureServices((ctx, services) =>
    {
        services
            .AddLogging()
            .AddSingleton<IConsumerConfiguration, ConsumerConfiguration>()
            .AddKafkaFlowHostedService(kafka =>
            {
                var serviceConfig = new ConsumerConfiguration(ctx.Configuration);
                
                kafka.UseConsoleLog()
                    .AddCluster(cluster =>
                    {
                        cluster
                            .WithBrokers(new[] { serviceConfig.Broker })
                            .AddConsumer(consumer => consumer
                                .Topic(serviceConfig.DefaultTopic)
                                .WithGroupId(serviceConfig.GroupId)
                                .WithBufferSize(serviceConfig.BufferSize)
                                .WithWorkersCount(serviceConfig.WorkerCount)
                                .AddMiddlewares(middlewares => middlewares
                                    .AddSerializer<NewtonsoftJsonSerializer>()
                                    .AddTypedHandlers(handlers => handlers
                                        .AddHandler<TestMessageHandler>()
                                    )
                                )
                            );
                    });
            });
    })
    .Build()
    .Run();