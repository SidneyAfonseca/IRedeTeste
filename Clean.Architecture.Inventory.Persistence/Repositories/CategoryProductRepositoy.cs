
using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Inventory.Persistence.Repositories
{
	public class CategoryProductRepositoy : ICategoryProductRepository
	{
		private readonly InventoryControlDbContext _context;

		public CategoryProductRepositoy(InventoryControlDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(CategoryProduct categoryProduct)
		{
			await _context.CategoryProducts.AddAsync(categoryProduct);
		}

		public void Delete(CategoryProduct categoryProduct)
		{
			_context.CategoryProducts.Remove(categoryProduct);
		}

		public async Task<bool> ExistsAsync(int categoryId)
		{
			var category = await _context.CategoryProducts.FindAsync(categoryId);
			return category != null;
		}

		public async Task<IEnumerable<CategoryProduct>> GetAllAsync()
		{
			return await _context.CategoryProducts.ToListAsync();
		}

		public async Task<CategoryProduct> GetByIdAsync(int id)
		{
			return await _context.CategoryProducts.FindAsync(id);
		}

		public async Task<IEnumerable<CategoryProduct>> GetByName(string text)
		{
			return await _context.CategoryProducts
						 .Where(p => p.Name.Contains(text)).ToListAsync();
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();

		}

		public void Update(CategoryProduct categoryProduct)
		{
			_context.CategoryProducts.Update(categoryProduct);
		}
	}
}
