using Confluent.Kafka;
using System.Text.Json;
using IdentityServer.CustomAbstraction;
namespace IdentityServer.Services;

public partial class KafkaProduser : ITweetProducer
{
    private readonly ProducerConfig _config;
    private readonly string _topicName;

    public KafkaProduser(ProducerConfig producerConfig, string topicName)
    {
        _config = producerConfig;
        _topicName = topicName;
    }

    public async Task ProduceAsync<T>(T message, CancellationToken cancellationToken)
    {
        using var producer = new ProducerBuilder<string, string>(_config)
           .SetKeySerializer(Serializers.Utf8)
           .SetValueSerializer(Serializers.Utf8)
           .Build();

        var eventMessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(message, typeof(T))
        };

        var deliveryResult = await producer.ProduceAsync(_topicName, eventMessage, cancellationToken);

        if (deliveryResult.Status == PersistenceStatus.NotPersisted)
        {
            throw new Exception($"Could not produce {typeof(T)} message to topic - {_topicName} due to the following reason: {deliveryResult.Message}.");
        }
    }
}