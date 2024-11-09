using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Inventory.Application.Queries
{
    public class GetAllCategoryProductsQuery : IRequest<IEnumerable<CategoryProduct>>
    {
    }
}
