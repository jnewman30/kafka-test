// See https://aka.ms/new-console-template for more information

using KafkaFlow;
using KafkaFlow.Serializer;
using KafkaProducer.HostedServices;
using MessageTypes.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder()
    .ConfigureServices((ctx, services) =>
    {
        services
            .AddLogging()
            .AddKafkaFlowHostedService(kafka =>
            {
                var serviceConfig = new ProducerConfiguration(ctx.Configuration);

                kafka.UseConsoleLog()
                    .AddCluster(cluster =>
                    {
                        cluster
                            .WithBrokers(new[] { serviceConfig.Broker })
                            .AddProducer(serviceConfig.ProducerName, producer =>
                                producer
                                    .DefaultTopic(serviceConfig.DefaultTopic)
                                    .AddMiddlewares(middlewares =>
                                        middlewares.AddSerializer<NewtonsoftJsonSerializer>()));
                    });
            })
            .AddSingleton(p => p.CreateKafkaBus())
            .AddTransient<IProducerConfiguration, ProducerConfiguration>()
            .AddHostedService<PingProducerService>();
    })
    .UseConsoleLifetime()
    .Build()
    .Run();