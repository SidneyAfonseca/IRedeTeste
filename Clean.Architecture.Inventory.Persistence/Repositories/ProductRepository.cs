using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Inventory.Persistence.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly InventoryControlDbContext _context;

		public ProductRepository(InventoryControlDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Product product)
		{
			await _context.Products.AddAsync(product);
		}

		public void Delete(Product product)
		{
			_context.Products.Remove(product);
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			return await _context.Products.FindAsync(id);
		}

		public async Task<IEnumerable<Product>> GetByNameOrDescriptionAsync(string text)
		{
			return await _context.Products
						 .Where(p => p.Description.Contains(text) || p.Name.Contains(text))
						 .ToListAsync();
		}


		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void Update(Product product)
		{
			_context.Products.Update(product);
		}

		public async Task<IQueryable<Product>> GetAllQueryableAsync()
		{
			// Retorna um IQueryable de produtos sem o carregamento das categorias associadas
			return _context.Products;
		}
	}
}
