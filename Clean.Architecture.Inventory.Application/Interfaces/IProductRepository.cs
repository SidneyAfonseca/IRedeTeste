using Clean.Architecture.Inventory.Domain.Entities;

namespace Clean.Architecture.Inventory.Application.Interfaces
{
	public interface IProductRepository
	{
		Task<Product> GetByIdAsync(int id);
		Task<IEnumerable<Product>> GetByNameOrDescriptionAsync(string text);
		Task<IEnumerable<Product>> GetAllAsync();
		Task AddAsync(Product product);
		void Update(Product product);
		void Delete(Product product);
		Task SaveChangesAsync();
		Task<IQueryable<Product>> GetAllQueryableAsync();
	}
}
