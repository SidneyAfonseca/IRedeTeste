using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;
using Serilog;

namespace Clean.Architecture.Inventory.Application.Behaviors
{
    public class ErrorLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IErrorLogRepository _errorLogRepository;

        public ErrorLoggingBehavior(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                // Log no banco de dados
                var errorLog = new ErrorLog
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    OccurredAt = DateTime.UtcNow
                };
                await _errorLogRepository.AddAsync(errorLog);
                await _errorLogRepository.SaveChangesAsync();

                // Opcional: Log adicional com Serilog
                Log.Error(ex, "An error occurred while processing the request.");

                // Re-throw a exceção para que o middleware de erros possa capturá-la
                throw;
            }
        }
    }
}
