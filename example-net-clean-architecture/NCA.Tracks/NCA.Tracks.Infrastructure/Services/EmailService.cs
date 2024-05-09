using NCA.Common.Application.Infrastructure.Log;
using NCA.Production.Application.Models;
using NCA.Production.Application.Services;

namespace NCA.Production.Infrastructure.Services
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
