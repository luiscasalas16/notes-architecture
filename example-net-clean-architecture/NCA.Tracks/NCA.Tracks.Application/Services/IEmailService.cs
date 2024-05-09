using NCA.Production.Application.Models;

namespace NCA.Production.Application.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
