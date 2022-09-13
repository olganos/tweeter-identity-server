namespace IdentityServer.CustomAbstraction
{
    public interface ITweetProducer
    {
        Task ProduceAsync<T>(T message, CancellationToken cancellationToken);
    }
}