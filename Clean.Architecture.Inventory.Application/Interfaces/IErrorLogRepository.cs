using Clean.Architecture.Inventory.Domain.Entities;

namespace Clean.Architecture.Inventory.Application.Interfaces
{
    public interface IErrorLogRepository
    {
        Task AddAsync(ErrorLog log);
        Task SaveChangesAsync();
    }
}
