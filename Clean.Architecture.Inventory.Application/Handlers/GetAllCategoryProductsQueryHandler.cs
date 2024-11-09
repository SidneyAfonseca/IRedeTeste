using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Application.Queries;
using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Inventory.Application.Handlers
{
    public class GetAllCategoryProductsQueryHandler : IRequestHandler<GetAllCategoryProductsQuery, IEnumerable<CategoryProduct>>
    {
        private readonly ICategoryProductRepository _categoryproductRepository;

        public GetAllCategoryProductsQueryHandler(ICategoryProductRepository productRepository)
        {
            _categoryproductRepository = productRepository;
        }

        public async Task<IEnumerable<CategoryProduct>> Handle(GetAllCategoryProductsQuery request, CancellationToken cancellationToken)
        {
            return await _categoryproductRepository.GetAllAsync();
        }
    }
}
