using System.Text.Json;
using Some.Root.DefaultNamespace.Options;

using Confluent.Kafka;
using Microsoft.Extensions.Options;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace Some.Root.DefaultNamespace.Handlers;

public class SomeHandler : BackgroundService
{
    private readonly ConsumerConfig _config;
    private readonly string _topic;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new();

    public SomeHandler(IOptions<KafkaOptions> kafkaOptions)
    {
        _config = new()
        {
            GroupId = kafkaOptions.Value.GroupId,
            BootstrapServers = kafkaOptions.Value.ConnectionString,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoOffsetStore = false
        };
        _topic = kafkaOptions.Value.Topic;

        _jsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
        consumer.Subscribe(_topic);
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

                var consumeResult = consumer.Consume(cancellationToken);
                Console.WriteLine(
                    $"Message: {consumeResult.Message.Value} received from {consumeResult.TopicPartitionOffset}");
            }
        }
        catch (Exception)
        {
            consumer.Close();
        }
    }
}
