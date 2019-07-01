using System.Threading.Tasks;

namespace UltraCryptoFolio.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailAddress, string subject, string message);
    }
}
