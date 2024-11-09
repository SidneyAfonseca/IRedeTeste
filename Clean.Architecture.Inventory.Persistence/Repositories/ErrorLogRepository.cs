using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Domain.Entities;

namespace Clean.Architecture.Inventory.Persistence.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly InventoryControlDbContext _context;

        public ErrorLogRepository(InventoryControlDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ErrorLog log)
        {
            await _context.ErrorLogs.AddAsync(log);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
