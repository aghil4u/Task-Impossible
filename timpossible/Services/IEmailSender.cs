using System.Threading.Tasks;

namespace timpossible.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}