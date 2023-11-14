// DummyEmailSender.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

public class DummyEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Dummy implementasjon - gjør ingenting
        return Task.CompletedTask;
    }
}
