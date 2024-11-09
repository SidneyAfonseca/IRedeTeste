using Clean.Architecture.Inventory.Application.Commands;
using Clean.Architecture.Inventory.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Inventory.Application.Handlers
{
	public class PathProductCommandHandler : IRequestHandler<PathProductCommand, Unit>
	{

		private readonly IProductRepository _productRepository;

		public PathProductCommandHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<Unit> Handle(PathProductCommand request, CancellationToken cancellationToken)
		{
			var product = await _productRepository.GetByIdAsync(request.Id);
			if (product == null)
			{
				throw new Exception($"Product with ID {request.Id} not found.");
			}

			product.Price = request.Price;
			product.UpdatedAt = DateTime.UtcNow;

			_productRepository.Update(product);
			await _productRepository.SaveChangesAsync();

			return Unit.Value; // Retorna Unit.Value para indicar conclusão
		}
	}
}
