using NCA.Common.Application.Infrastructure.Log;
using NCA.Tracks.Application.Models;
using NCA.Tracks.Application.Services;

namespace NCA.Tracks.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;

        public EmailService(ILogger logger)
        {
            _logger = logger;
        }

        public Task<bool> SendEmail(Email email)
        {
            _logger.LogError("Se envió el email.");

            return Task.FromResult(true);
        }
    }
}
