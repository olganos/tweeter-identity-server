using Microsoft.AspNetCore.Identity.UI.Services;

namespace IdentityServer.Services;

public class DummyEmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return;
    }
}
