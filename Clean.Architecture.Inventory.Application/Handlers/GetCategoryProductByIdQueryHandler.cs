using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Application.Queries;
using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Inventory.Application.Handlers
{
    public class GetCategoryProductByIdQueryHandler : IRequestHandler<GetCategoryProductByIdQuery, CategoryProduct>
    {
        private readonly ICategoryProductRepository _categoryproductRepository;

        public GetCategoryProductByIdQueryHandler(ICategoryProductRepository productRepository)
        {
            _categoryproductRepository = productRepository;
        }

        public async Task<CategoryProduct> Handle(GetCategoryProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _categoryproductRepository.GetByIdAsync(request.Id);
            return product;
        }
    }
}
