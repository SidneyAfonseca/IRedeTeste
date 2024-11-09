using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Domain.Entities;

namespace Clean.Architecture.Inventory.Infrastructure.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly IErrorLogRepository _errorLogRepository;

        public LoggingService(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public async Task LogErrorAsync(string message, string stackTrace)
        {
            var errorLog = new ErrorLog
            {
                Message = message,
                StackTrace = stackTrace,
                OccurredAt = DateTime.UtcNow
            };

            await _errorLogRepository.AddAsync(errorLog);
            await _errorLogRepository.SaveChangesAsync();
        }
    }
}
