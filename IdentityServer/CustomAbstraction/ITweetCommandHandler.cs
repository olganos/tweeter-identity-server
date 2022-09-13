using static IdentityServer.Services.KafkaProduser;

namespace IdentityServer.CustomAbstraction
{
    public interface ITweetCommandHandler
    {
        Task SendCommandAsync(AddUserCommand command, CancellationToken cancellationToken);
    }
}
