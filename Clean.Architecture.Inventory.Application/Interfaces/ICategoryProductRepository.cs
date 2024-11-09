

using Clean.Architecture.Inventory.Domain.Entities;

namespace Clean.Architecture.Inventory.Application.Interfaces
{
	public interface ICategoryProductRepository
	{
		Task<bool> ExistsAsync(int categoryId);
		Task<CategoryProduct> GetByIdAsync(int id);
		Task<IEnumerable<CategoryProduct>> GetByName(string text);
		Task<IEnumerable<CategoryProduct>> GetAllAsync();
		Task AddAsync(CategoryProduct categoryProduct);
		void Update(CategoryProduct categoryProduct);
		void Delete(CategoryProduct categoryProduct);
		Task SaveChangesAsync();
	}
}
