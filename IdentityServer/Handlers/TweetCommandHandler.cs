using IdentityServer.CustomAbstraction;

using static IdentityServer.Services.KafkaProduser;

namespace IdentityServer.Handlers;

public class TweetCommandHandler : ITweetCommandHandler
{
    private readonly ITweetProducer _producer;

    public TweetCommandHandler(ITweetProducer producer)
    {
        _producer = producer;
    }

    public async Task SendCommandAsync(AddUserCommand command, CancellationToken cancellationToken)
    {
        await _producer.ProduceAsync(command, cancellationToken);
    }
}
