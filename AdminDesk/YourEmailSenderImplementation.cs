using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

public class YourEmailSenderImplementation : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Implementer logikken for å sende e-post her
        // Dette kan gjøres ved hjelp av SmtpClient, SendGrid, eller andre e-posttjenester

        // Eksempel med SmtpClient
        using (SmtpClient client = new SmtpClient())
        {
            // Konfigurer SmtpClient og send e-post
            // For eksempel:
            // client.Host = "smtp.example.com";
            // client.Send(...);
        }

        return Task.CompletedTask;
    }
}
