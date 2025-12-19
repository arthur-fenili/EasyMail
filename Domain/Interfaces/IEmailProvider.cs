namespace Domain.Interfaces;

public interface IEmailProvider
{
    Task SendAsync(string to, string subject, string body, bool isHtml = false);
}
