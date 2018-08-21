using System.Threading.Tasks;

namespace Scheduler.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
