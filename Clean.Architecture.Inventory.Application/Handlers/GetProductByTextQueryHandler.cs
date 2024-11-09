using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;
using Clean.Architecture.Inventory.Application.Queries;

namespace Clean.Architecture.Inventory.Application.Handlers
{
	public class GetProductByTextQueryHandler : IRequestHandler<GetProductByTextQuery, IEnumerable<Product>>
	{
		private readonly IProductRepository _productRepository;

		public GetProductByTextQueryHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<IEnumerable<Product>> Handle(GetProductByTextQuery request, CancellationToken cancellationToken)
		{
			var product = await _productRepository.GetByNameOrDescriptionAsync(request.Text);
			return product;
		}
	}
}
