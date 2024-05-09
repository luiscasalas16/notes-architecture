using NCA.Tracks.Application.Models;

namespace NCA.Tracks.Application.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
