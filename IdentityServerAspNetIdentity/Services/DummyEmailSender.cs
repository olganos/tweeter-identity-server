using Microsoft.AspNetCore.Identity.UI.Services;

namespace IdentityServerAspNetIdentity.Services;

public class DummyEmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return;
    }
}
