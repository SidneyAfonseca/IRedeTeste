using Clean.Architecture.Inventory.Application.Commands;
using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;
using Serilog;

namespace Clean.Architecture.Inventory.Application.Handlers
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
	{
		private readonly IProductRepository _productRepository;

		public CreateProductCommandHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			int id = 0;
			try
			{
				var product = new Product
				{
					Name = request.Name,
					Price = request.Price,
					Photo = request.Photo,
					Description = request.Descritpion,
					ExpirationDate = request.ExpirationDate,
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow,
					CategoryId = request.CategoryId
				};

				await _productRepository.AddAsync(product);
				await _productRepository.SaveChangesAsync();
				id = product.Id;
			}
			catch (Exception ex)
			{

				Log.Error(ex, "An error occurred while processing the request.");
			}

			return id;

		}
	}
}
